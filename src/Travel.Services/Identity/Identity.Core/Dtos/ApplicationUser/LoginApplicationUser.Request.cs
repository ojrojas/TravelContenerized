namespace Identity.Core.Dtos;

public record LoginApplicationUserRequest: BaseRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string ClientId { get; set; }
    public IEnumerable<string> Scopes { get; set; }
}

