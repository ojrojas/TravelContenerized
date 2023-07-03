namespace Identity.Core.Dtos;

public record DeleteApplicationUserRequest: BaseRequest
{
	public Guid Id { get; set; }
}

