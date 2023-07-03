namespace Library.Core.Dtos;

public record CreateBookRequest : BaseRequest
{
    public Book Book { get; set; } = null!;
}

