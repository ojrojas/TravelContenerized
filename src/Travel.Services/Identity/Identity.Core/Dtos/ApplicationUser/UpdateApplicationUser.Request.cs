namespace Identity.Core.Dtos;

public record UpdateApplicationUserRequest: BaseRequest
{
	public ApplicationUser ApplicationUser { get; set; } = null!;


}

