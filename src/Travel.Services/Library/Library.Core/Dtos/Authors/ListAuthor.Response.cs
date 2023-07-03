namespace Library.Core.Dtos;

public record ListAuthorResponse : BaseResponse
{
    public ListAuthorResponse(Guid correlation) : base(correlation)
    {
    }

    public IEnumerable<Author> Authors { get; set; } = null!;
}

