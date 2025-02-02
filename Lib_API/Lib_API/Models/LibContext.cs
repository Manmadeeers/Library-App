using Microsoft.EntityFrameworkCore;
namespace Lib_API.Models
{
    public class LibContext:DbContext
    {
        public LibContext(DbContextOptions<LibContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Author { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasIndex(b => b.ISBN).IsUnique();
            modelBuilder.Entity<Book>().HasOne(b=>b.BookAuthor).WithMany(a=>a.Books).HasForeignKey(b=>b.AuthorId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
