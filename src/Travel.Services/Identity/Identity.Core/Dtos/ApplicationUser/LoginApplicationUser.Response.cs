namespace Identity.Core.Dtos;

public record LoginApplicationUserResponse: BaseResponse
{
	public LoginApplicationUserResponse(Guid correlationId): base(correlationId) { }
	public string Token { get; set; } = null!;
	public IResult ActionResult { get; set; } = null!;
}