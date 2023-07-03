namespace Library.Core.Entities
{
    public class AuthorsBooks
    {
        public Guid Id { get; set; }
        public Author Author { get; set; }
        public Guid AuthorId { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
