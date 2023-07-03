namespace Identity.Core.Dtos;

public record GetByIdApplicationUserRequest: BaseRequest
{
	public Guid Id { get; set; } 
}

