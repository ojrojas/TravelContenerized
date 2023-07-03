namespace Library.Core.Dtos;

public record DeleteAuthorRequest : BaseRequest
{
    public Guid Id { get; set; }
}

