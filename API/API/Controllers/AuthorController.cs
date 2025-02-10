using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly LibDbContext _context;

        public AuthorController(LibDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAllAuthors()
        {
            return await _context.authors.Include(a=>a.Books).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Author>>GetAuthorById(int id)
        {
            var author = await _context.authors.Include(a=>a.Books).FirstOrDefaultAsync(a=>a.Id==id);
            return author != null ? author : NotFound();

        }

        [HttpPost]
        public async Task<ActionResult<Author>>CreateAuthor(Author author)
        {
            _context.authors.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAuthorById), new { id = author.Id }, author);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Author>>DeleteAuthor(int id)
        {
            var authrow = await _context.authors.FindAsync(id);
            if(authrow == null)
            {
                return NotFound();
            }
            _context.authors.Remove(authrow);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
