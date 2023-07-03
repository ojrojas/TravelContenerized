namespace Library.Core.Dtos;

public record CreateBookResponse : BaseResponse
{
    public CreateBookResponse(Guid correlation) : base(correlation)
    {
    }

    public Book BookCreated { get; set; } = null!;
}

