namespace Lib_API.Models
{
    public class Author
    {
        public int AuthId { get; set; }
        public string Name { get; set; }
        public string Surename { get; set; }

        public string BirthDate { get; set; }

        public string CountryOfOrigin { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();

    }
}
