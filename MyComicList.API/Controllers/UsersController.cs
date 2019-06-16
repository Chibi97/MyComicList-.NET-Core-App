using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyComicList.API.Filters;
using MyComicList.Application.Commands.Users;
using MyComicList.Application.DataTransfer.Users;
using MyComicList.Application.Exceptions;
using MyComicList.Application.Requests;
using MyComicList.Application.Responses;

namespace MyComicList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IGetUsers getCommand;
        private readonly IGetOneUser getOneCommand;
        private readonly IAddUser addCommand;
        private readonly IUpdateUser updateCommand;
        private readonly IDeleteUser deleteCommand;

        public UsersController(IGetUsers getCommand, IGetOneUser getOneCommand, IAddUser addCommand,
            IUpdateUser updateCommand, IDeleteUser deleteCommand)
        {
            this.getCommand = getCommand;
            this.getOneCommand = getOneCommand;
            this.addCommand = addCommand;
            this.updateCommand = updateCommand;
            this.deleteCommand = deleteCommand;
        }

        // GET: api/Users
        [HttpGet]
        [LoggedIn]
        public IActionResult Get([FromQuery] UserRequest request)
        {
            var result = getCommand.Execute(request);
            return Ok(result);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        [LoggedIn]
        public IActionResult Get(int id)
        {
            try
            {
                var comic = getOneCommand.Execute(id);
                return Ok(comic);
            } catch(EntityNotFoundException e)
            {
                return NotFound(new ErrorMessage { Message = e.Message });
            }
            
        }
        //POST: api/Users
        [HttpPost]
        public IActionResult Post([FromBody] UserCreateDTO user)
        {
            try
            {
                addCommand.Execute(user);
                return Ok();

            } catch(EntityAlreadyExistsException e)
            {
                return Conflict(new ErrorMessage { Message = e.Message });
            }
        }


        // PUT: api/Users/5
        [HttpPut("{id}")]
        [LoggedIn("Admin")]
        public IActionResult Put(int id, [FromBody] UserUpdateDTO user)
        {
            try
            {
                user.UserId = id;
                updateCommand.Execute(user);
                return NoContent();

            }
            catch (EntityAlreadyExistsException e)
            {
                return Conflict(new ErrorMessage { Message = e.Message });
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new ErrorMessage { Message = e.Message });
            }
        }

        // DELETE: api/ApiWithActions/5
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
        }
    }
}
