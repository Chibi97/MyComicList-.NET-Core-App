using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyComicList.API.Filters;

namespace MyComicList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        // GET: api/Publishers
        [HttpGet]
        [LoggedIn]
        public IActionResult Get()
        {
            return Ok();
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

        //// POST: api/Authors
        //[HttpPost]
        //[LoggedIn("Admin")]
        //public IActionResult Post([FromBody] AuthorAddDTO author)
        //{
        //    addCommand.Execute(author);
        //    return Ok();
        //}

        //// PUT: api/Authors/5
        //[HttpPut("{id}")]
        //[LoggedIn("Admin")]
        //public IActionResult Put(int id, [FromBody] AuthorUpdateDTO author)
        //{
        //    try
        //    {
        //        author.Id = id;
        //        updateCommand.Execute(author);
        //        return NoContent();
        //    }
        //    catch (EntityNotFoundException e)
        //    {
        //        return NotFound(new ErrorMessage { Message = e.Message });
        //    }
        //}

        //// DELETE: api/Authors/5
        //[HttpDelete("{id}")]
        //[LoggedIn("Admin")]
        //public IActionResult Delete(int id)
        //{
        //    try
        //    {
        //        deleteCommand.Execute(id);
        //        return NoContent();
        //    }
        //    catch (EntityNotFoundException e)
        //    {
        //        return NotFound(new ErrorMessage { Message = e.Message });
        //    }
        //}
    }
}
