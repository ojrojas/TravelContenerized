namespace Library.Core.Dtos;

public record CreateAuthorResponse : BaseResponse
{
    public CreateAuthorResponse(Guid correlation) : base(correlation)
    {
    }

    public Author AuthorCreated { get; set; } = null!;
}

