using System.Collections.Generic;
using System.Linq;
using EventApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using netcore_gyakorlas.Models;
using netcore_gyakorlas.UnitOfWork;

namespace netcore_gyakorlas.Services
{
    
    public interface IBookService
    {
        IEnumerable<Book> GetAll();
        Book Get(int id);
        Book Create(Book newBook);
        Book Update(int bookId, Book updatedBook);
        IEnumerable<Book> Delete(int bookId);
        
        public Book GetByTitle(string title);
        IEnumerable<Book> GetByAuthor(int authorId);

        IEnumerable<Book> GetByYear(int year);
    }
    
    
    public class BookService : AbstractService, IBookService
    {
        
        private readonly ILogger<BookService> _logger;
        
        public BookService(IUnitOfWork unitOfWork, ILogger<BookService> logger) : base(unitOfWork)
        {
            _logger = logger;
        }

        private void Log(string message)
        {
            _logger.LogInformation("PlaceService log: " + message);
        }

        
        public IEnumerable<Book> GetAll()
        {
            Log("GetAll");
            return UnitOfWork.GetRepository<Book>().GetAll();
        }

        public Book Get(int id)
        {
            Log("Get(" + id + ")");
            return UnitOfWork.GetRepository<Book>()
                .GetByIdWithInclude(id, src => src.Include(home => home.Author));
        }
        
        public Book Create(Book newBook)
        {
            Log("Create");
            newBook.Id = 0;
            UnitOfWork.GetRepository<Book>().Create(newBook);
            UnitOfWork.SaveChanges();
            return newBook;
        }

        public Book Update(int bookId, Book updatedBook)
        {
            Log("Update(" + bookId + ")");
            UnitOfWork.GetRepository<Book>().Update(bookId, updatedBook);
            UnitOfWork.SaveChanges();
            return updatedBook;
        }

        public IEnumerable<Book> Delete(int bookId)
        {
            Log("Delete(" + bookId + ")");
            UnitOfWork.GetRepository<Book>().Delete(bookId);
            UnitOfWork.SaveChanges();
            return GetAll();
        }
        
        public Book GetByTitle(string title)
        {
            Log("GetByTitle(" + title + ")");
            return UnitOfWork.GetRepository<Book>().GetAsQueryable()
                .Where(book => book.Title.Equals(title))
                .Include(book => book.Author)
                .First();
        }

        public IEnumerable<Book> GetByAuthor(int authorId)
        {
            Log("GetByAuthor(" + authorId + ")");
            return UnitOfWork.GetRepository<Book>().GetAsQueryable()
                .Where(book => book.Author.Id.Equals(authorId))
                .Include(book => book.Author);
        }

        public IEnumerable<Book> GetByYear(int year)
        {
            Log("GetByYear(" + year + ")");
            return UnitOfWork.GetRepository<Book>().GetAsQueryable()
                .Where(book => book.PublishedYear.Equals(year));
        }
    }
}