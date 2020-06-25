using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookClubApi.Models;

namespace BookClubApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        // GET: api/Books
        [HttpGet]
        public IActionResult GetBooks()
        {
            var result = _bookRepository.AllBooks;
            return Ok(result);
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public IActionResult GetBook(int id)
        {
            var book = _bookRepository.GetBookById(id);

            if (book == null)
            {
                return NotFound(id);
            }

            return Ok(book);
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutBook(int id, Book book)
        {
            if (id != book.BookId)
            {
                return BadRequest();
            }

            try
            {
                _bookRepository.UpdateBook(id, book);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(book);
        }

        // POST: api/Books
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public IActionResult PostBook(Book book)
        {
            var newBook = _bookRepository.AddBook(book);

            return Ok(newBook);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _bookRepository.DeleteBook(id);
            return Ok(book);
        }

        private bool BookExists(int id)
        {
            return _bookRepository.AllBooks.Any(e => e.BookId == id);
        }
    }
}
