namespace Library.Core.Dtos;

public record UpdateEditorialResponse : BaseResponse
{
    public UpdateEditorialResponse(Guid correlation) : base(correlation)
    {
    }

    public Editorial EditorialUpdated { get; set; } = null!;
}

