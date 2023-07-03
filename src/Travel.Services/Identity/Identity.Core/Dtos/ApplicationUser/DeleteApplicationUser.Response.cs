namespace Identity.Core.Dtos;

public record DeleteApplicationUserResponse : BaseResponse
{
	public DeleteApplicationUserResponse(Guid correlationId) : base(correlationId)
	{
	}

	public ApplicationUser? ApplicationUserDeleted { get; set; }
}

