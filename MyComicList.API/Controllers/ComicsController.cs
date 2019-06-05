using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyComicList.Application.Commands.Comics;
using MyComicList.Application.Exceptions;
using MyComicList.Application.Requests;

namespace MyComicList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComicsController : ControllerBase
    {
        private readonly IGetComics getCommand;
        private readonly IGetOneComic getOneCommand;

        public ComicsController(IGetComics getCommand, IGetOneComic getOneCommand)
        {
            this.getCommand = getCommand;
            this.getOneCommand = getOneCommand;
        }
        
        [HttpGet] // GET: api/Comics
        public IActionResult Get([FromQuery]ComicRequest request)
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
                return NotFound(e.Message);
            }
        }

        //// POST: api/Comics
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

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
