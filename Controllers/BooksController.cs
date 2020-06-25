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
        private readonly AppDbContext _context;

        public BooksController(IBookRepository bookRepository, AppDbContext context)
        {
            _bookRepository = bookRepository;
            _context = context;
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
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.BookId)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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

            return NoContent();
        }

        // POST: api/Books
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBook), new { id = book.BookId }, book);
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
