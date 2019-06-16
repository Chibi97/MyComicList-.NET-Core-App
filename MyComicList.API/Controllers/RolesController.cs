using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyComicList.API.Filters;
using MyComicList.Application.Commands.Roles;
using MyComicList.Application.DataTransfer.Roles;
using MyComicList.Application.Exceptions;
using MyComicList.Application.Responses;
using MyComicList.DataAccess;

namespace MyComicList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IAddRole addCommand;

        public MyComicListContext Context { get; }

        public RolesController(MyComicListContext context, IAddRole addCommand)
        {
            Context = context;
            this.addCommand = addCommand;
        }

        // GET: api/Roles
        [HttpGet]
        [LoggedIn("Admin")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Roles/5
        [HttpGet("{id}", Name = "Get")]
        [LoggedIn("Admin")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Roles
        [HttpPost]
        [LoggedIn("Admin")]
        public IActionResult Post([FromBody] RoleAddDTO request)
        {
            try
            {
                addCommand.Execute(request);
                return StatusCode(201);
            } catch(EntityAlreadyExistsException e)
            {
                return Conflict(new ErrorMessage { Message = e.Message });
            }
        }

        // PUT: api/Roles/5
        [HttpPut("{id}")]
        [LoggedIn("Admin")]

        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        [LoggedIn("Admin")]
        public void Delete(int id)
        {
        }
    }
}
