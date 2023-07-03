namespace Library.Core.Dtos;

public record UpdateBookRequest : BaseRequest
{
    public Book Book { get; set; } = null!;
}

