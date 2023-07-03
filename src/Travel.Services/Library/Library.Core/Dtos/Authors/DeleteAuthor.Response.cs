namespace Library.Core.Dtos;

public record DeleteAuthorResponse : BaseResponse
{
    public DeleteAuthorResponse(Guid correlation) : base(correlation)
    {
    }

    public Author AuthorDeleted { get; set; } = null!;
}

