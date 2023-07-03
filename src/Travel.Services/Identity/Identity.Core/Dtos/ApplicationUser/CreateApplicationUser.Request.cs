
namespace Identity.Core.Dtos;

public record CreateApplicationUserRequest: BaseRequest
{
	public ApplicationUser ApplicationUser { get; set; } = null!;
}

