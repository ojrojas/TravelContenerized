namespace Identity.Core.Dtos;

public record GetAllApplicationUserResponse: BaseResponse
{
	public GetAllApplicationUserResponse(Guid correlationId): base(correlationId)
	{
	}

	public IEnumerable<ApplicationUser>? ApplicationsUsers { get; set; }
}

