using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyComicList.API.Filters;
using MyComicList.Application.Commands.Genres;
using MyComicList.Application.DataTransfer.Genres;
using MyComicList.Application.Exceptions;
using MyComicList.Application.Responses;
using MyComicList.DataAccess;

namespace MyComicList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IAddGenre addCommand;
        private readonly IUpdateGenre updateCommand;
        private readonly IDeleteGenre deleteCommand;

        public MyComicListContext Context { get; }

        public GenresController(MyComicListContext context, IAddGenre addCommand, IUpdateGenre updateCommand, IDeleteGenre deleteCommand)
        {
            Context = context;
            this.addCommand = addCommand;
            this.updateCommand = updateCommand;
            this.deleteCommand = deleteCommand;
        }

        // GET: api/Genres
        [HttpGet]
        //[LoggedIn]
        public IActionResult Get()
        {
            var genres = Context.Genres
                .Where(g => g.DeletedAt == null)
                .OrderBy(g => g.Id)
                .Select(g => new GenreDTO
                {
                    Id = g.Id,
                    Name = g.Name,
                });

            return Ok(genres);
        }

        // GET: api/Genres/5
        [HttpGet("{id}")]
        //[LoggedIn]
        public IActionResult Get(int id)
        {
            var genre = Context.Genres
                .Where(g => g.DeletedAt == null && g.Id == id)
                .Select(g => new GenreDTO
                {
                    Id = g.Id,
                    Name = g.Name
                }).SingleOrDefault();
            if (genre == null) return NotFound(new ErrorMessage { Message = $"Genre - not valid, Given value: { id } is not found" });

            return Ok(genre);
        }

        // POST: api/Genres
        [HttpPost]
        [LoggedIn("Admin")]
        public IActionResult Post([FromBody] GenreAddDTO genre)
        {
            try
            {
                addCommand.Execute(genre);
                return StatusCode(201);

            } catch(EntityAlreadyExistsException e)
            {
                return Conflict(new ErrorMessage { Message = e.Message });
            }

        }

        // PUT: api/Genres/5
        [HttpPut("{id}")]
        [LoggedIn("Admin")]
        public IActionResult Put(int id, [FromBody] GenreDTO genre)
        {
            try
            {
                genre.Id = id;
                updateCommand.Execute(genre);
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

        // DELETE: api/genres/5
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
