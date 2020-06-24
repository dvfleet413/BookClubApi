using System.Collections.Generic;
using System.Linq;

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
    }
}