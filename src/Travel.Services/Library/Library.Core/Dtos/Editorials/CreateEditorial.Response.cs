namespace Library.Core.Dtos;

public record CreateEditorialResponse : BaseResponse
{
    public CreateEditorialResponse(Guid correlation) : base(correlation)
    {
    }

    public Editorial EditorialCreated { get; set; } = null!;
}

