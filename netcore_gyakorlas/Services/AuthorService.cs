using System.Collections.Generic;
using System.Linq;
using EventApp.Services;
using Microsoft.Extensions.Logging;
using netcore_gyakorlas.Models;
using netcore_gyakorlas.UnitOfWork;

namespace netcore_gyakorlas.Services
{
    
    public interface IAuthorService
    {
        IEnumerable<Author> GetAll();
        Author Get(int id);
        Author Create(Author newBook);
        Author Update(int bookId, Author updatedBook);
        Author GetByName(string name);
        IEnumerable<Author> Delete(int bookId);
    }
    
    
    public class AuthorService : AbstractService, IAuthorService
    {
        
        private readonly ILogger<AuthorService> _logger;
        
        public AuthorService(IUnitOfWork unitOfWork, ILogger<AuthorService> logger) : base(unitOfWork)
        {
            _logger = logger;
        }

        private void Log(string message)
        {
            _logger.LogInformation("PlaceService log: " + message);
        }

        
        public IEnumerable<Author> GetAll()
        {
            Log("GetAll");
            return UnitOfWork.GetRepository<Author>().GetAll();
        }

        public Author Get(int id)
        {
            Log("Get(" + id + ")");
            return UnitOfWork.GetRepository<Author>().GetById(id).Result;
        }
        
        public Author Create(Author newAuthor)
        {
            Log("Create");
            newAuthor.Id = 0;
            UnitOfWork.GetRepository<Author>().Create(newAuthor);
            UnitOfWork.SaveChanges();
            return newAuthor;
        }

        public Author Update(int authorId, Author updatedAuthor)
        {
            Log("Update(" + authorId + ")");
            UnitOfWork.GetRepository<Author>().Update(authorId, updatedAuthor);
            UnitOfWork.SaveChanges();
            return updatedAuthor;
        }

        public Author GetByName(string name)
        {
            Log("GetByName(" + name + ")");
            return UnitOfWork.GetRepository<Author>().GetAsQueryable()
                .First(author => author.Name.Equals(name));
        }

        public IEnumerable<Author> Delete(int authorId)
        {
            Log("Delete(" + authorId + ")");
            UnitOfWork.GetRepository<Author>().Delete(authorId);
            UnitOfWork.SaveChanges();
            return GetAll();
        }
    }
}