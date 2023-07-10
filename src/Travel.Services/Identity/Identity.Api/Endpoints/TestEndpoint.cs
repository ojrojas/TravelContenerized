namespace Identity.Api.Endpoints;

[ApiController]
[Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
public class TestEndpoint : ControllerBase
{

	[HttpGet("getinfouser")]
	public IActionResult GetInfoUser()
	{
		return Ok("Hi workd this is a test endpoint with authorize header");
	}
}

