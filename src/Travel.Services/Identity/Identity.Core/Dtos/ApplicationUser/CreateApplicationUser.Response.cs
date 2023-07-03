namespace Identity.Core.Dtos;

public record CreateApplicationUserResponse : BaseResponse
{
    public CreateApplicationUserResponse(Guid correlationId) : base(correlationId) { }
    public ApplicationUser? ApplicationUserCreated { get; set; }
}