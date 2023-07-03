namespace Library.Core.Dtos;

public record UpdateAuthorRequest : BaseRequest
{
    public Author Author { get; set; } = null!;
}

