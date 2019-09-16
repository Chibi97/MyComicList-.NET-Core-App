using System;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyComicList.API.Filters;
using MyComicList.Application.Commands.Authors;
using MyComicList.Application.DataTransfer.Authors;
using MyComicList.Application.Exceptions;
using MyComicList.Application.Responses;
using MyComicList.DataAccess;

namespace MyComicList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAddAuthor addCommand;
        private readonly IUpdateAuthor updateCommand;
        private readonly IDeleteAuthor deleteCommand;

        public AuthorsController(MyComicListContext context, IAddAuthor addCommand, IUpdateAuthor updateCommand, IDeleteAuthor deleteCommand)
        {
            Context = context;
            this.addCommand = addCommand;
            this.updateCommand = updateCommand;
            this.deleteCommand = deleteCommand;
        }

        public MyComicListContext Context { get; }

        // GET: api/Authors
        [HttpGet]
        //[LoggedIn]
        public IActionResult Get()
        {
            var authors = Context.Authors
                .Where(a => a.DeletedAt == null)
                .OrderBy(a => a.Id)
                .Select(a => new AuthorGetDTO
                {
                   Id = a.Id,
                   FullName = a.FirstName + ' ' + a.LastName
                });
            return Ok(authors);
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        //[LoggedIn]
        public IActionResult Get(int id)
        {
            var author = Context.Authors
                .Where(a => a.DeletedAt == null && a.Id == id)
                .Select(a => new AuthorGetDTO
                {
                    Id = a.Id,
                    FullName = a.FirstName + ' ' + a.LastName
                }).SingleOrDefault();
            if (author == null) return NotFound(new ErrorMessage { Message = $"Author - not valid, Given value: { id } is not found" });

            return Ok(author);
        }

        // POST: api/Authors
        [HttpPost]
        [LoggedIn("Admin")]
        public IActionResult Post([FromBody] AuthorAddDTO author)
        {
            addCommand.Execute(author);
            return StatusCode(201);
        }

        // PUT: api/Authors/5
        [HttpPut("{id}")]
        [LoggedIn("Admin")]
        public IActionResult Put(int id, [FromBody] AuthorUpdateDTO author)
        {
            try
            {
                author.Id = id;
                updateCommand.Execute(author);
                return NoContent();
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new ErrorMessage { Message = e.Message });
            }
        }

        // DELETE: api/Authors/5
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
