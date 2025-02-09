namespace API.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surename { get; set; }
        public DateTime BirthDate { get; set; }
        public string CountryOfOrigin { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();  
    }
}
