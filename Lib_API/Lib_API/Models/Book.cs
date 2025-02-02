namespace Lib_API.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public int AuthorId {get; set; }

        public long ISBN { get; set; }

        public string Title { get; set; }
        public string Genre { get; set; }

        public string Description { get; set; }

        public Author BookAuthor { get; set; }

        public DateTime? TakeTime { get; set; }

    }
}
