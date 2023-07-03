namespace Library.Core.Dtos;
public record DeleteEditorialResponse : BaseResponse
{
    public DeleteEditorialResponse(Guid correlation) : base(correlation)
    {
    }

    public Editorial EditorialDeleted { get; set; } = null!;
}

