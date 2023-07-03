namespace Library.Core.Dtos;

public record DeleteBookResponse : BaseResponse
{
    public DeleteBookResponse(Guid correlation) : base(correlation)
    {
    }

    public Book BookDeleted { get; set; } = null!;
}

