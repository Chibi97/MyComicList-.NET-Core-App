using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private readonly List<string> allowedFileUploadTypes;
        private readonly IGetComics getCommand;
        private readonly IGetOneComic getOneCommand;
        private readonly IAddComic addCommand;
        private readonly IUpdateComic updateCommand;
        private readonly IDeleteComic deleteCommand;
        
        public ComicsController(IConfiguration config ,IGetComics getCommand, IGetOneComic getOneCommand,
            IAddComic addCommand, IUpdateComic updateCommand, IDeleteComic deleteCommand)
        {
            this.allowedFileUploadTypes = config.GetSection("AllowedFileUploadTypes")
                                                .AsEnumerable().Where(c => c.Value != null)
                                                .Select(c => c.Value)
                                                .ToList();
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
        public IActionResult Post([FromForm] ComicAddDTO comic)
        {
            string extenstion = Path.GetExtension(comic.Image.FileName);
            if(!allowedFileUploadTypes.Contains(extenstion))
            {
                return UnprocessableEntity(new ErrorMessage { Message = "Image extension is not allowed." });
            }

            try
            {
                var fileName = Guid.NewGuid().ToString() + "_" + comic.Image.FileName;
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);

                comic.Image.CopyTo(new FileStream(uploadPath, FileMode.Create));
                comic.ImagePath = $"https://{HttpContext.Request.Host}/uploads/{fileName}";
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
        public IActionResult Put(int id, [FromForm] ComicUpdateDTO comic)
        {
            if(comic.Image != null)
            {
                string extenstion = Path.GetExtension(comic.Image.FileName);
                if (!allowedFileUploadTypes.Contains(extenstion))
                {
                    return UnprocessableEntity(new ErrorMessage { Message = "Image extension is not allowed." });
                }
            }

            try
            {
                if(comic.Image != null)
                {
                    var fileName = Guid.NewGuid().ToString() + "_" + comic.Image.FileName;
                    var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);

                    comic.Image.CopyTo(new FileStream(uploadPath, FileMode.Create));
                    comic.ImagePath = $"https://{HttpContext.Request.Host}/uploads/{fileName}";
                }

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

        /// <summary>
        /// Deletes a specific Comic. 
        /// Only for admin authenticated with a token
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /comics/1
        ///
        /// </remarks>
        /// <param name="id"></param> 
        /// <response code="204">If succedes in deletion</response>
        /// <response code="404">If comic doesn't exit</response>
        [HttpDelete("{id}")] // DELETE: api/Comics/5
        [LoggedIn("Admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
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
