namespace Identity.Core.Dtos;

public record GetByIdApplicationUserResponse: BaseResponse
{
	public GetByIdApplicationUserResponse(Guid correlationId): base(correlationId)
	{
	}

	public ApplicationUser? ApplicationUser { get; set; }
}

