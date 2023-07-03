namespace Library.Core.Dtos;

public record UpdateBookResponse : BaseResponse
{
    public UpdateBookResponse(Guid correlation) : base(correlation)
    {
    }

    public Book BookUpdated { get; set; } = null!;
}

