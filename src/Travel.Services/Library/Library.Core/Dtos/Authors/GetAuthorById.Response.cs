namespace Library.Core.Dtos;

public record GetAuthorByIdResponse : BaseResponse
{
    public GetAuthorByIdResponse(Guid correlation) : base(correlation)
    {
    }

    public Author AuthorFound { get; set; } = null!;
}

