using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly LibDbContext _context;

        public BookController(LibDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks()
        {
            return await _context.books.Include(b=>b.BookAuthor).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>>GetBookById(int id)
        {
            var book = await _context.books.Include(b=>b.BookAuthor).FirstOrDefaultAsync(b=>b.Id== id);
            return book != null ? book : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Book>>CreateBook(Book book)
        {
            _context.books.Add(book);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>>DeleteBookById(int id)
        {
            var book = await _context.books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            _context.books.Remove(book);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
