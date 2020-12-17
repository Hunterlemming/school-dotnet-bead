using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using netcore_gyakorlas.Models;
using netcore_gyakorlas.Services;

namespace netcore_gyakorlas.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class BookLibraryController : ControllerBase
    {
        private readonly IBookLibraryService _bookLibraryService;

        public BookLibraryController(IBookLibraryService bookLibraryService)
        {
            _bookLibraryService = bookLibraryService;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<BookLibrary>> GetAll()
        {
            return Ok(_bookLibraryService.GetAll());
        }

        [HttpPost]
        public IActionResult Create([FromBody] BookLibrary newBookLibrary)
        {
            var bookLibrary = _bookLibraryService.Create(newBookLibrary);
            return Created($"{bookLibrary.Id}", bookLibrary);
        }
    }
}