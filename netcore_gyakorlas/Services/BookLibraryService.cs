using System.Collections.Generic;
using System.Linq;
using EventApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using netcore_gyakorlas.Models;
using netcore_gyakorlas.UnitOfWork;

namespace netcore_gyakorlas.Services
{
    
    public interface IBookLibraryService
    {
        IEnumerable<BookLibrary> GetAll();
        BookLibrary Create(BookLibrary newBookLibrary);
    }
    
    public class BookLibraryService : AbstractService, IBookLibraryService
    {
        
        private readonly ILogger<LibraryService> _logger;
        private readonly IMemoryCache _memoryCache;
        
        public BookLibraryService(IUnitOfWork unitOfWork, ILogger<LibraryService> logger, IMemoryCache memoryCache) : base(unitOfWork)
        {
            _memoryCache = memoryCache; 
            _logger = logger;
        }

        private void Log(string message)
        {
            _logger.LogInformation("PlaceService log: " + message);
        }
        
        public IEnumerable<BookLibrary> GetAll()
        {
            if (!_memoryCache.TryGetValue("bookLibraries", out IEnumerable<BookLibrary> bookLibraries))
            {
                return refreshBookLibraries();
            }
            Log("GetAll");
            return bookLibraries;
        }

        public BookLibrary Create(BookLibrary newBookLibrary)
        {
            Log("Create");
            newBookLibrary.Id = 0;
            
            UnitOfWork.GetRepository<BookLibrary>().Create(newBookLibrary);
            UnitOfWork.SaveChanges();

            refreshBookLibraries();
            
            return newBookLibrary;
        }

        private IEnumerable<BookLibrary> refreshBookLibraries()
        {
            var bookLibraries = UnitOfWork.GetRepository<BookLibrary>().GetAll()
                .Include(bl => bl.Library)
                .Include(bl => bl.Book).ToList();
            _memoryCache.Set("bookLibraries", bookLibraries);
            return bookLibraries;
        }
    }
}