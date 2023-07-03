namespace Identity.Core.Dtos;

public record UpdateApplicationUserResponse: BaseResponse
{
	public UpdateApplicationUserResponse(Guid correlationId): base(correlationId)
	{
	}

	public ApplicationUser? ApplicationUserUpdated { get; set; }
}

