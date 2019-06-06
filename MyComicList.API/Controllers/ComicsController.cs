﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyComicList.Application.Commands.Comics;
using MyComicList.Application.DataTransfer;
using MyComicList.Application.Exceptions;
using MyComicList.Application.Requests;
using MyComicList.Application.Responses;
using MyComicList.Domain;

namespace MyComicList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComicsController : ControllerBase
    {
        private readonly IGetComics getCommand;
        private readonly IGetOneComic getOneCommand;
        private readonly IAddComic addComic;

        public ComicsController(IGetComics getCommand, IGetOneComic getOneCommand, IAddComic addComic)
        {
            this.getCommand = getCommand;
            this.getOneCommand = getOneCommand;
            this.addComic = addComic;
        }
        
        [HttpGet] // GET: api/Comics
        public IActionResult Get([FromQuery] ComicRequest request)
        {
            var result = getCommand.Execute(request);
            return Ok(result);
        }

   
        [HttpGet("{id}")] // GET: api/Comics/5
        public IActionResult Get(int id)
        {
            try
            {
                var comic = getOneCommand.Execute(id);
                return Ok(comic);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new ErrorMessage { Message = e.Message});
            }
        }

        
        [HttpPost] // POST: api/Comics
        public IActionResult Post([FromBody] ComicDTO comic)
        {
            addComic.Execute(comic);
            return Ok();
        }

        //// PUT: api/Comics/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
