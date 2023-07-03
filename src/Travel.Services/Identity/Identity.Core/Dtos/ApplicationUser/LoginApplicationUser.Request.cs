namespace Identity.Core.Dtos;

public record LoginApplicationUserRequest: BaseRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
}

