namespace API.Models
{
    public class Book
    {
        public int Id { get; set; }
        public int AuthorsID { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public DateTime? TakeTime { get; set; }
        public Author BookAuthor { get; set; }
    }
}
