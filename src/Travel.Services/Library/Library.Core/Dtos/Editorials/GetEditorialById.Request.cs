namespace Library.Core.Dtos;

public record GetEditorialByIdRequest : BaseRequest
{
    public Guid Id { get; set; }
}

