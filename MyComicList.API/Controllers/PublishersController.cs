using System;
using System.Collections.Generic;
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

namespace MyComicList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly IGetPublishers getCommand;
        private readonly IAddPublisher addCommand;
        private readonly IUpdatePublisher updateCommand;

        public PublishersController(IGetPublishers getCommand, IAddPublisher addCommand, IUpdatePublisher updateCommand)
        {
            this.getCommand = getCommand;
            this.addCommand = addCommand;
            this.updateCommand = updateCommand;
        }

        // GET: api/Publishers
        [HttpGet]
        [LoggedIn]
        public IActionResult Get(PublisherRequest request)
        {
            var result = getCommand.Execute(request);
            return Ok(result);
        }

        //// GET: api/Authors/5
        //[HttpGet("{id}")]
        //[LoggedIn]
        //public IActionResult Get(int id)
        //{
        //    try
        //    {
        //        var author = Context.Authors
        //            .Where(a => a.DeletedAt == null && a.Id == id)
        //            .Select(a => new AuthorGetDTO
        //            {
        //                Id = a.Id,
        //                FullName = a.FirstName + ' ' + a.LastName
        //            });

        //        return Ok(author);

        //    }
        //    catch (EntityNotFoundException e)
        //    {
        //        return NotFound(new ErrorMessage { Message = e.Message });
        //    }
        //}

        // POST: api/Authors
        [HttpPost]
        [LoggedIn("Admin")]
        public IActionResult Post([FromBody] PublisherAddDTO author)
        {
            try
            {
                addCommand.Execute(author);
                return Ok();
            } catch(EntityAlreadyExistsException e)
            {
                return NotFound(new ErrorMessage { Message = e.Message });
            }
        }

        // PUT: api/Authors/5
        [HttpPut("{id}")]
        [LoggedIn("Admin")]
        public IActionResult Put(int id, [FromBody] PublisherDTO publisher)
        {
            try
            {
                publisher.Id = id;
                updateCommand.Execute(publisher);
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
                //deleteCommand.Execute(id);
                return NoContent();
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new ErrorMessage { Message = e.Message });
            }
        }
    }
}
