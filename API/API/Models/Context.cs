using Microsoft.EntityFrameworkCore;
namespace API.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }
        public DbSet<Book> books { get; set; }
        public DbSet<Author>authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
            .HasIndex(b => b.ISBN)
            .IsUnique();

            modelBuilder.Entity<Book>()
            .HasOne(b => b.BookAuthor)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorsID)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
