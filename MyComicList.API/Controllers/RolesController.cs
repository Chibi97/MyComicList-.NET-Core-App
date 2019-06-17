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
        private readonly IUpdateRole updateCommand;
        private readonly IDeleteRole deleteCommand;

        public MyComicListContext Context { get; }

        public RolesController(MyComicListContext context, IAddRole addCommand, IUpdateRole updateCommand, IDeleteRole deleteCommand)
        {
            Context = context;
            this.addCommand = addCommand;
            this.updateCommand = updateCommand;
            this.deleteCommand = deleteCommand;
        }

        // GET: api/Roles
        [HttpGet]
        [LoggedIn("Admin")]
        public IActionResult Get()
        {
            var roles = Context.Roles
               .Where(r => r.DeletedAt == null)
               .OrderBy(r => r.Id)
               .Select(r => new RoleDTO
               {
                   Id = r.Id,
                   Name = r.Name,
               });

            return Ok(roles);
        }

        // GET: api/Roles/5
        [HttpGet("{id}", Name = "Get")]
        [LoggedIn("Admin")]
        public IActionResult Get(int id)
        {
            var role = Context.Roles
                .Where(r => r.DeletedAt == null && r.Id == id)
                .Select(r => new RoleDTO
                {
                    Id = r.Id,
                    Name = r.Name
                }).SingleOrDefault();
            if (role == null) return NotFound(new ErrorMessage { Message = $"Role - not valid, Given value: { id } is not found" });

            return Ok(role);
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
            }
            catch (EntityAlreadyExistsException e)
            {
                return Conflict(new ErrorMessage { Message = e.Message });
            }
        }

        // PUT: api/Roles/5
        [HttpPut("{id}")]
        [LoggedIn("Admin")]

        public IActionResult Put(int id, [FromBody] RoleDTO role)
        {
            try
            {
                role.Id = id;
                updateCommand.Execute(role);
                return NoContent();
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new ErrorMessage { Message = e.Message });
            }
            catch (EntityAlreadyExistsException e)
            {
                return Conflict(new ErrorMessage { Message = e.Message });
            }
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        [LoggedIn("Admin")]
        public IActionResult Delete(int id)
        {
            try
            {
                deleteCommand.Execute(id);
                return NoContent();
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new ErrorMessage { Message = e.Message });
            }
            catch (NotEmptyCollectionException e)
            {
                return UnprocessableEntity(new ErrorMessage { Message = e.Message });
            }
        }
    }
}
