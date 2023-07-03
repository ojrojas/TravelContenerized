using Travel.BaseHttps.BaseEndpoints;

namespace Library.Core.Dtos;

public record UpdateAuthorResponse : BaseResponse
{
    public UpdateAuthorResponse(Guid correlation) : base(correlation) { }
    public Author AuthorUpdated { get; set; } = null!;
}

