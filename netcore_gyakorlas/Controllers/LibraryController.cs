using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using netcore_gyakorlas.Models;
using netcore_gyakorlas.Services;

namespace netcore_gyakorlas.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryService _libraryService;

        public LibraryController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<Library>> GetAll()
        {
            return Ok(_libraryService.GetAll());
        }

        [HttpPost]
        public IActionResult Create([FromBody] Library newLibrary)
        {
            var library = _libraryService.Create(newLibrary);
            return Created($"{library.Id}", library);
        }
    }
}