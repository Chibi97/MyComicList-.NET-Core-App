using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyComicList.API.Filters;
using MyComicList.API.Services;
using MyComicList.Application.Commands.MyList;
using MyComicList.Application.DataTransfer.MyList;
using MyComicList.Application.Exceptions;
using MyComicList.Application.Requests;
using MyComicList.Application.Responses;

namespace MyComicList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyListController : ControllerBase
    {
        private readonly ILoginService loginService;
        private readonly IAddToMyList addCommand;
        private readonly IDeleteFromMyList deleteCommand;
        private readonly IGetMyList getCommand;
        private readonly IGetOneFromMyList getOneCommand;

        public MyListController(ILoginService loginService, IAddToMyList addCommand, IDeleteFromMyList deleteCommand, IGetMyList getCommand, IGetOneFromMyList getOneCommand)
        {
            this.loginService = loginService;
            this.addCommand = addCommand;
            this.deleteCommand = deleteCommand;
            this.getCommand = getCommand;
            this.getOneCommand = getOneCommand;
        }

        // GET: api/MyList
        [HttpGet]
        [LoggedIn]
        public IActionResult Get([FromQuery]MyListGetRequest request)
        {
            request.User = loginService.PossibleUser();
            var result = getCommand.Execute(request);
            return Ok(result);
        }

        // GET: api/MyList/5
        [HttpGet("{id}")]
        [LoggedIn]
        public IActionResult Get(int id)
        {
            try
            {
                var dto = new MyListDTO();
                dto.User = loginService.PossibleUser();
                dto.ComicId = id;
                var comic = getOneCommand.Execute(dto);
                return Ok(comic);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new ErrorMessage { Message = e.Message });
            }
        }


        // POST: api/MyList
        [HttpPost]
        [LoggedIn]
        public IActionResult Post([FromBody] MyListAddRequest request)
        {
            try
            {
                var dto = new MyListAddDTO();
                dto.User = loginService.PossibleUser();
                dto.Comics = request.Comics;
                 addCommand.Execute(dto);
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


        // DELETE: api/myList/5
        [HttpDelete("{id}")]
        [LoggedIn]
        public IActionResult Delete(int id)
        {
            try
            {
                var dto = new MyListDTO();
                dto.User = loginService.PossibleUser();
                dto.ComicId = id;
                deleteCommand.Execute(dto);
                return NoContent();
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new ErrorMessage { Message = e.Message });
            }
        }
    }
}
