namespace Library.Core.Dtos;

public record ListBooksResponse : BaseResponse
{
    public ListBooksResponse(Guid correlation) : base(correlation)
    {
    }

    public IEnumerable<Book> Books { get; set; } = null!;
}

