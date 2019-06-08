using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyComicList.Application.Commands.Comics;
using MyComicList.Application.DataTransfer.Comics;
using MyComicList.Application.Exceptions;
using MyComicList.Application.Requests;
using MyComicList.Application.Responses;

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
                return NotFound(new ErrorMessage { Message = e.Message });
            }
        }

        
        [HttpPost] // POST: api/Comics
        public IActionResult Post([FromBody] ComicCreateDTO comic)
        {
            try
            {
                addComic.Execute(comic);
                return Ok();
            }
            catch(EntityAlreadyExistsException e)
            {
                return NotFound(new ErrorMessage { Message = e.Message });
            }
            catch(EntityNotFoundException e)
            {
                return BadRequest(new ErrorMessage { Message = e.Message});
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
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
