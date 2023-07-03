namespace Identity.Core.Dtos;

public record LogoutApplicationUserResponse: BaseResponse
{
	public LogoutApplicationUserResponse(Guid correlationId): base(correlationId)
	{
	}

	public string LogoutDescription { get; set; } = null!;
}

