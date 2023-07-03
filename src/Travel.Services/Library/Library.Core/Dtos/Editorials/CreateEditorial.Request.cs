namespace Library.Core.Dtos;

public record CreateEditorialRequest : BaseRequest
{
    public Editorial Editorial { get; set; } = null!;
}

