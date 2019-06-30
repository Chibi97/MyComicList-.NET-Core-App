using Microsoft.Extensions.Configuration;
using MyComicList.DataAccess;
using System;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using MyComicList.Application.Exceptions;
using MyComicList.Application.DataTransfer.Auth;

namespace MyComicList.Shared.Services
{
    public class JWTUserService : ITokenService<int, UserLoginDTO>
    {
        private MyComicListContext context;
        private IConfiguration config;
        private readonly IPasswordService passwordService;

        public JWTUserService(MyComicListContext context, IConfiguration config, IPasswordService passwordService)
        {
            this.context = context;
            this.config = config;
            this.passwordService = passwordService;
        }
        
        // returns user id retrieved from a decrypted token
        public int Decrypt(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            try
            {
                handler.ValidateToken(token, Policy(), out SecurityToken validToken);
                var jwt = handler.ReadToken(token) as JwtSecurityToken;
                var id = jwt.Claims.First(c => c.Type == "userId").Value;
                return int.Parse(id);

            } catch(Exception)
            {
                throw new InvalidTokenException();
            }
        }

        // generates JWT token based on a request, returns an encrypted token
        public string Encrypt(UserLoginDTO request)
        {
            var user = context.Users.Where(u => u.Username == request.Username.Trim() && u.DeletedAt == null).SingleOrDefault();
            if (user == null) throw new UnauthorizedAccessException();

            if (!passwordService.Verify(request.Password, user.Password))
            {
               throw new UnauthorizedAccessException();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(config.GetSection("Encryption")["key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim("userId", user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private TokenValidationParameters Policy()
        {
            var key = Encoding.ASCII.GetBytes(config.GetSection("Encryption")["key"]);
            return new TokenValidationParameters()
            {
                RequireExpirationTime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        }
    }
}
