using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyComicList.API.Filters;
using MyComicList.Application.Commands.Publishers;
using MyComicList.Application.DataTransfer.Publishers;
using MyComicList.Application.Exceptions;
using MyComicList.Application.Requests;
using MyComicList.Application.Responses;
using MyComicList.DataAccess;

namespace MyComicList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly IGetPublishers getCommand;
        private readonly IAddPublisher addCommand;
        private readonly IUpdatePublisher updateCommand;
        private readonly IDeletePublisher deleteCommand;

        public MyComicListContext Context { get; }

        public PublishersController(MyComicListContext context,IGetPublishers getCommand, IAddPublisher addCommand, IUpdatePublisher updateCommand, IDeletePublisher deleteCommand)
        {
            Context = context;
            this.getCommand = getCommand;
            this.addCommand = addCommand;
            this.updateCommand = updateCommand;
            this.deleteCommand = deleteCommand;
        }

        // GET: api/Publishers
        [HttpGet]
        [LoggedIn]
        public IActionResult Get([FromQuery]PublisherRequest request)
        {
            var result = getCommand.Execute(request);
            return Ok(result);
        }

        // GET: api/Publishers/5
        [HttpGet("{id}")]
        [LoggedIn]
        public IActionResult Get(int id)
        {
            var publisher = Context.Publishers
                .Where(p => p.DeletedAt == null && p.Id == id)
                .Select(p => new PublisherDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Origin = p.Origin
                }).SingleOrDefault();
            if (publisher == null) return NotFound(new ErrorMessage { Message = $"Publisher - not valid, Given value: { id } is not found" });

            return Ok(publisher);
        }

        // POST: api/Publishers
        [HttpPost]
        [LoggedIn("Admin")]
        public IActionResult Post([FromBody] PublisherAddDTO author)
        {
            try
            {
                addCommand.Execute(author);
                return StatusCode(201);
            } catch(EntityAlreadyExistsException e)
            {
                return NotFound(new ErrorMessage { Message = e.Message });
            }
        }

        // PUT: api/Publishers/5
        [HttpPut("{id}")]
        [LoggedIn("Admin")]
        public IActionResult Put(int id, [FromBody] PublisherDTO publisher)
        {
            try
            {
                //var path = Directory.GetParent(Directory.GetCurrentDirectory()).ToString();
                //var testPath = Path.Combine(path, "imeprojekta.api", "wwws");
                //Console.WriteLine("test");

                publisher.Id = id;
                updateCommand.Execute(publisher);
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

        // DELETE: api/Publishers/5
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
            catch(NotEmptyCollectionException e)
            {
                return UnprocessableEntity(new ErrorMessage { Message = e.Message });
            }
        }
    }
}
