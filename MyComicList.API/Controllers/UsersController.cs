﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public UsersController(IGetUsers getCommand, IGetOneUser getOneCommand, IAddUser addCommand, IUpdateUser updateCommand)
        {
            this.getCommand = getCommand;
            this.getOneCommand = getOneCommand;
            this.addCommand = addCommand;
            this.updateCommand = updateCommand;
        }

        // GET: api/Users
        [HttpGet]
        public IActionResult Get([FromQuery] UserRequest request)
        {
            var result = getCommand.Execute(request);
            return Ok(result);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
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
            catch (EntityNotFoundException e)
            {
                return NotFound(new ErrorMessage { Message = e.Message });
            }
        }


        // PUT: api/Users/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserUpdateDTO user)
        {
            try
            {
                user.UserId = id;
                updateCommand.Execute(user);
                return NoContent();

            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new ErrorMessage { Message = e.Message });
            }
        }

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}