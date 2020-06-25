using System.Collections.Generic;

namespace BookClubApi.Models
{
    public interface IBookRepository 
    {
        IEnumerable<Book> AllBooks { get; }
        Book CurrentBook { get; }
        Book AddBook(Book book);
        Book GetBookById(int BookId);
        Book DeleteBook(int BookId);
    }
}