using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Controllers
{
    [Route("api/Book")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly Context _context;

        public BooksController(Context context)
        {
            _context = context;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks()
        {
            return await _context.books
                .Include(b => b.BookAuthor)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.books
                .Include(b => b.BookAuthor)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null) return NotFound();
            return book;
        }

        [HttpGet("isbn/{isbn}")]
        public async Task<ActionResult<Book>> GetBookByIsbn(long isbn)
        {
            var book = await _context.books
                .Include(b => b.BookAuthor)
                .FirstOrDefaultAsync(b => b.ISBN == isbn);

            if (book == null) return NotFound();
            return book;
        }

        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook(Book book)
        {
            _context.books.Add(book);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, Book book)
        {
            if (id != book.Id) return BadRequest();

            _context.Entry(book).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.books.FindAsync(id);
            if (book == null) return NotFound();

            _context.books.Remove(book);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("{id}/borrow")]
        public async Task<IActionResult> BorrowBook(int id)
        {
            var book = await _context.books.FindAsync(id);
            if (book == null) return NotFound();

            if (book.TakeTime.HasValue)
                return BadRequest("Book is already borrowed");

            book.TakeTime = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("{id}/return")]
        public async Task<IActionResult> ReturnBook(int id)
        {
            var book = await _context.books.FindAsync(id);
            if (book == null) return NotFound();

            if (!book.TakeTime.HasValue)
                return BadRequest("Book is not borrowed");

            book.TakeTime = null;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

 
