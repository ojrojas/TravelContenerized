namespace Identity.Api.Endpoints;

public static class ApplicationUserEndpoints
{
	public static RouteGroupBuilder ApplicationUserEndpointsGroup(this RouteGroupBuilder group)
	{
        group.MapPost("/login", [AllowAnonymous] async (
    [FromServices] IApplicationUserService _service, LoginApplicationUserRequest _request, CancellationToken cancellationToken) => {
        return await _service.LoginAsync(_request, cancellationToken);
    });

        group.MapPost("/token", async (HttpContext context, IOpenIddictApplicationManager _applicationManager, IOpenIddictScopeManager _scopeManager) =>
        {
            var request = context.GetOpenIddictServerRequest() ?? throw new InvalidOperationException("Error request operation not found clientcredentials");
            if (request.IsClientCredentialsGrantType())
            {
                ArgumentNullException.ThrowIfNull(request.ClientId);

                var application = await _applicationManager.FindByClientIdAsync(request.ClientId);
                if (application == null)
                {
                    throw new InvalidOperationException("The application details cannot be found in the database.");
                }

                // Create the claims-based identity that will be used by OpenIddict to generate tokens.
                var identity = new ClaimsIdentity(
                    authenticationType: TokenValidationParameters.DefaultAuthenticationType,
                    nameType: Claims.Name,
                    roleType: Claims.Role);

                // Add the claims that will be persisted in the tokens (use the client_id as the subject identifier).
                identity.SetClaim(Claims.Subject, await _applicationManager.GetClientIdAsync(application));
                identity.SetClaim(Claims.Name, await _applicationManager.GetDisplayNameAsync(application));

                identity.SetScopes(request.GetScopes());
                identity.SetResources(await _scopeManager.ListResourcesAsync(identity.GetScopes()).ToListAsync());
                identity.SetDestinations(GetDestinations);

                return Results.SignIn(new ClaimsPrincipal(identity), new(), OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }

            throw new NotImplementedException("The specified grant type is not implemented.");
        });

        group.MapMethods("/connect/authorize", new[] { HttpMethods.Get, HttpMethods.Post }, async (HttpContext context, IOpenIddictApplicationManager _applicationManager, IOpenIddictScopeManager _scopeManager) => {
            var request = context.GetOpenIddictServerRequest() ?? throw new InvalidOperationException("Error request operation not found clientcredentials");
            var result = await context.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (!result.Succeeded)
            {
                return Results.Challenge(
                    properties: new AuthenticationProperties
                    {
                        RedirectUri = context.Request.PathBase + context.Request.Path + QueryString.Create(
                           context.Request.HasFormContentType ? context.Request.Form.ToList() : context.Request.Query.ToList())
                    }, new List<string> { CookieAuthenticationDefaults.AuthenticationScheme });
            }

            ArgumentNullException.ThrowIfNull(request.ClientId);

            var application = await _applicationManager.FindByClientIdAsync(request.ClientId);
            if (application == null)
            {
                throw new InvalidOperationException("The application details cannot be found in the database.");
            }

            // Create the claims-based identity that will be used by OpenIddict to generate tokens.
            var identity = new ClaimsIdentity(
                authenticationType: TokenValidationParameters.DefaultAuthenticationType,
                nameType: Claims.Name,
                roleType: Claims.Role);

            // Add the claims that will be persisted in the tokens (use the client_id as the subject identifier).
            identity.SetClaim(Claims.Subject, await _applicationManager.GetClientIdAsync(application));
            identity.SetClaim(Claims.Name, await _applicationManager.GetDisplayNameAsync(application));

            identity.SetScopes(request.GetScopes());
            identity.SetResources(await _scopeManager.ListResourcesAsync(identity.GetScopes()).ToListAsync());
            identity.SetDestinations(GetDestinations);

            return Results.SignIn(new ClaimsPrincipal(identity), new(), OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        });

        group.MapPost("/logout", [Authorize]
        async (HttpContext context, [FromServices] ILoggerFactory logFactory, string urlReturn) => {
            var _logger = logFactory.CreateLogger("Logout ApplicationUser");
            _logger.LogInformation("Request logout");
            LogoutApplicationUserRequest request = new();
            LogoutApplicationUserResponse response = new(request.CorrelationId());
            await context.SignOutAsync();
            _logger.LogInformation("Logout successful");
            response.LogoutDescription = "Loggout successful";
            return Results.Redirect(urlReturn);
        });

        return group;
	}

    private static IEnumerable<string> GetDestinations(Claim claim)
    {
        return claim.Type switch
        {
            Claims.Name or
            Claims.Subject
                => new[] { Destinations.AccessToken, Destinations.IdentityToken },

            _ => new[] { Destinations.AccessToken },
        };
    }
}

