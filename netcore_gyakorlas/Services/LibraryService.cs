using System.Collections.Generic;
using System.Linq;
using EventApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using netcore_gyakorlas.Models;
using netcore_gyakorlas.UnitOfWork;

namespace netcore_gyakorlas.Services
{
    
    public interface ILibraryService
    {
        public IEnumerable<Library> GetAll();
        public Library Get(int id);
        public Library Create(Library newLibrary);
    }
    
    public class LibraryService : AbstractService, ILibraryService
    {
        
        private readonly ILogger<LibraryService> _logger;
        
        public LibraryService(IUnitOfWork unitOfWork, ILogger<LibraryService> logger) : base(unitOfWork)
        {
            _logger = logger;
        }

        private void Log(string message)
        {
            _logger.LogInformation("PlaceService log: " + message);
        }
        
        public IEnumerable<Library> GetAll()
        {
            Log("GetAll");
            return UnitOfWork.GetRepository<Library>().GetAll();
        }

        public Library Get(int id)
        {
            Log("Get(" + id + ")");
            return UnitOfWork.GetRepository<Library>().GetById(id).Result;
        }
        
        public Library Create(Library newLibrary)
        {
            Log("Create");
            newLibrary.Id = 0;
            UnitOfWork.GetRepository<Library>().Create(newLibrary);
            UnitOfWork.SaveChanges();
            return newLibrary;
        }
    }
}