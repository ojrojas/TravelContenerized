namespace Identity.Api.Endpoints;

public static class AuthorizationEndpoints
{
	public static RouteGroupBuilder AuthorizationEndpointsGroup(this RouteGroupBuilder group)
	{
        group.MapGet("/authorize", async (HttpContext context) =>
        {
            // Resolve the claims stored in the cookie created after the GitHub authentication dance.
            // If the principal cannot be found, trigger a new challenge to redirect the user to GitHub.
            var principal = (await context.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme))?.Principal;
            if (principal is null)
            {
                var properties = new AuthenticationProperties
                {
                    RedirectUri = context.Request.GetEncodedUrl()
                };

                return Results.Challenge(properties, new[] { OpenIddictClientAspNetCoreDefaults.AuthenticationScheme });
            }

            var identifier = principal.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            // Create the claims-based identity that will be used by OpenIddict to generate tokens.
            var identity = new ClaimsIdentity(
                authenticationType: TokenValidationParameters.DefaultAuthenticationType,
                nameType: Claims.Name,
                roleType: Claims.Role);

            // Import a few select claims from the identity stored in the local cookie.
            identity.AddClaim(new Claim(Claims.Subject, identifier));
            identity.AddClaim(new Claim(Claims.Name, identifier).SetDestinations(Destinations.AccessToken));

            return Results.SignIn(new ClaimsPrincipal(identity), properties: null, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        });

        return group;
	}
}

