namespace Aggregator.Api.Entities;

/// <summary>
/// Model Editoral 
/// </summary>
public class Editorial : BaseEntity, IAggregateRoot
{
    [StringLength(45)]
    public string Name { get; set; } = null!;
    [StringLength(45)]
    public string Sede { get; set; } = null!;
    public ICollection<Book> Books { get; set; } = null!;
}
