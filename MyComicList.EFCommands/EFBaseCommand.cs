﻿using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using MyComicList.DataAccess;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace MyComicList.EFCommands
{
    public abstract class EFBaseCommand
    {
        protected MyComicListContext Context { get; }

        protected EFBaseCommand(MyComicListContext context) => Context = context;

        protected string MakeHashPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
            ));

            return hashed;
        }
    }
}
