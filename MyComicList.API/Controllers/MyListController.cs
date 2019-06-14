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

        public MyListController(ILoginService loginService, IAddToMyList addCommand)
        {
            this.loginService = loginService;
            this.addCommand = addCommand;
        }

        //// GET: api/MyList
        //[HttpGet]
        //public IActionResult Get()
        //{
        //    return Ok();
        //}

        //// GET: api/MyList/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}


        // POST: api/MyList
        [HttpPost]
        //[LoggedIn]
        public IActionResult Post([FromBody] MyListRequest request)
        {
            try
            {
                var dto = new MyListAddDTO();
                dto.User = loginService.PossibleUser();
                dto.Comics = request.Comics;
                 addCommand.Execute(dto);
                return Ok();
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


        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
