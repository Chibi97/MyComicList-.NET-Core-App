using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyComicList.Application.Commands.Comics;
using MyComicList.Application.Requests;

namespace MyComicList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComicsController : ControllerBase
    {
        private readonly IGetComics getComics;

        public ComicsController(IGetComics getComics)
        {
            this.getComics = getComics;
        }
        // GET: api/Comics
        [HttpGet]
        public IActionResult Get(ComicRequest request)
        {
            var result = getComics.Execute(request);
            return Ok(result);
        }

        // GET: api/Comics/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Comics
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Comics/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
