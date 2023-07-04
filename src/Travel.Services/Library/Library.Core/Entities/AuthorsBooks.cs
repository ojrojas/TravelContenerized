namespace Library.Core.Entities;

/// <summary>
/// Model authors books
/// </summary>
public class AuthorsBooks
{
    public Guid Id { get; set; }
    public Author Author { get; set; } = null!;
    public Guid AuthorId { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; } = null!;
}
