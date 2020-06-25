using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BookClubApi.Models
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _appDbContext;
        public BookRepository(AppDbContext appdbContext)
        {
            _appDbContext = appdbContext;
        }
        public IEnumerable<Book> AllBooks
        {
            get
            {
                return _appDbContext.Books;
            }   
        }

        public Book CurrentBook
        {
            get
            {
                return _appDbContext.Books.FirstOrDefault(b => b.IsCurrentBook);
            }
        }

        public Book GetBookById(int BookId)
        {
            return _appDbContext.Books.FirstOrDefault(b => b.BookId == BookId);
        }

        public Book AddBook(Book book)
        {
            _appDbContext.Books.Add(book);
            _appDbContext.SaveChanges();
            return book;
        }

        public Book UpdateBook(int BookId, Book book)
        {
            _appDbContext.Entry(book).State = EntityState.Modified;
            _appDbContext.SaveChanges();
            return book;
        }

        public Book DeleteBook(int BookId)
        {
            var book = GetBookById(BookId);
            _appDbContext.Books.Remove(book);
            _appDbContext.SaveChanges();
            return book;
        }

        
    }
}