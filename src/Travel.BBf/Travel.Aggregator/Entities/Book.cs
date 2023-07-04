namespace Aggregator.Api.Entities;

/// <summary>
/// Model Book
/// </summary>
public class Book : IAggregateRoot
{
    [Key]
    public int ISBN { get; set; }
    [StringLength(45)]
    public string Title { get; set; } = null!;
    public string Sipnosis { get; set; } = null!;
    public int Pages { get; set; }
    public Guid EditorialId { get; set; }
    public Editorial Editorial { get; set; } = null!;
    public ICollection<AuthorsBooks> AuthorsBooks { get; set; } = null!;
}
