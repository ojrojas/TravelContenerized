namespace Library.Core.Dtos;

public record GetBookByIdResponse : BaseResponse
{
    public GetBookByIdResponse(Guid correlation) : base(correlation)
    {
    }

    public Book BookFound { get; set; } = null!;
}

