using System;
using Microsoft.AspNetCore.Mvc;
using MyComicList.API.Filters;
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
        private readonly IAddComic addCommand;
        private readonly IUpdateComic updateCommand;
        private readonly IDeleteComic deleteCommand;

        public ComicsController(IGetComics getCommand, IGetOneComic getOneCommand, IAddComic addCommand, IUpdateComic updateCommand, IDeleteComic deleteCommand)
        {
            this.getCommand = getCommand;
            this.getOneCommand = getOneCommand;
            this.addCommand = addCommand;
            this.updateCommand = updateCommand;
            this.deleteCommand = deleteCommand;
        }
        
        [HttpGet] // GET: api/Comics
        [LoggedIn]
        public IActionResult Get([FromBody]ComicRequest body, [FromQuery]PagedRequest queryParams)
        {
            var request = body;
            request.PerPage = queryParams.PerPage;
            request.Page = queryParams.Page;

            var result = getCommand.Execute(request);
            return Ok(result);
        }

   
        [HttpGet("{id}")] // GET: api/Comics/5
        [LoggedIn]
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
        [LoggedIn("Admin")]
        public IActionResult Post([FromBody] ComicCreateDTO comic)
        {
            try
            {
                addCommand.Execute(comic);
                return StatusCode(201);
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

        [HttpPut("{id}")] // PUT: api/Comics/5
        [LoggedIn("Admin")]
        public IActionResult Put(int id, [FromBody] ComicUpdateDTO comic)
        {
            try
            {
                comic.ComicId = id;
                updateCommand.Execute(comic);
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

        [HttpDelete("{id}")] // DELETE: api/Comics/5
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
