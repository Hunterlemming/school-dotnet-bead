using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using netcore_gyakorlas.Models;
using netcore_gyakorlas.Services;

namespace netcore_gyakorlas.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<Author>> GetAll()
        {
            return Ok(_authorService.GetAll());
        }

        [HttpPost]
        public IActionResult Create([FromBody] Author newAuthor)
        {
            var author = _authorService.Create(newAuthor);
            return Created($"{author.Id}", author);
        }
        
        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            return Ok(_authorService.GetByName(name));
        }
    }
}