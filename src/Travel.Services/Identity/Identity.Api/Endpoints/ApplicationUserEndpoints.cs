namespace Identity.Api.Endpoints;

public static class ApplicationUserEndpoints
{
	public static RouteGroupBuilder ApplicationUserEndpointsGroup(this RouteGroupBuilder group)
	{
        group.MapPost("/connect/token", [IgnoreAntiforgeryToken]
            async (HttpContext context,
                   IApplicationUserService _service,
                   IOpenIddictApplicationManager _applicationManager,
                   IOpenIddictScopeManager _scopeManager) =>
        {
            var request = context.GetOpenIddictServerRequest() ?? throw new InvalidOperationException();

            if (request.IsClientCredentialsGrantType())
            {
                return await CreateSignInLogin(_applicationManager, _scopeManager, request);
            }

            if(request.IsAuthorizationCodeGrantType())
            {
                var authentication = await context.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
                return await CreateSignInLogin(_applicationManager, _scopeManager, request, authentication);
            }

            if(request.IsPasswordGrantType())
            {
                ArgumentNullException.ThrowIfNull(request.ClientId, "Not found clientId request");
                ArgumentNullException.ThrowIfNull(request.Username, "Not found username request");
                ArgumentNullException.ThrowIfNull(request.Password, "Not found password request");

                var response = await _service.LoginAsync(
                    new() {
                        ClientId = request.ClientId,
                        UserName = request.Username,
                        Password = request.Password,
                        Scopes = request.GetScopes()
                    }, default);

                return response;
            }

            throw new NotImplementedException("The specified grant type is not implemented.");
        });

        group.MapMethods("/connect/authorize", 
        new[] { HttpMethods.Get, HttpMethods.Post }, 
        async (
            HttpContext context, 
            IOpenIddictApplicationManager _applicationManager,
            IOpenIddictScopeManager _scopeManager) => {
                
            var request = context.GetOpenIddictServerRequest() ?? throw new InvalidOperationException("Error request operation not found clientcredentials");
            var result = await context.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (!result.Succeeded)
            {
                var url = context.Request.PathBase + context.Request.Path + QueryString.Create(
                           context.Request.HasFormContentType ? context.Request.Form.ToList() : context.Request.Query.ToList());

                return Results.Challenge(
                    properties: new AuthenticationProperties
                    {
                        RedirectUri = url,
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
            identity.SetDestinations(GetDestination.GetDestinations);

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

    private static async Task<IResult> CreateSignInLogin(
        IOpenIddictApplicationManager _applicationManager,
        IOpenIddictScopeManager _scopeManager,
        OpenIddictRequest request,
        AuthenticateResult? authentication = null)
    {
        ArgumentNullException.ThrowIfNull(request.ClientId);

        var application = await _applicationManager.FindByClientIdAsync(request.ClientId);
        if (application == null)
        {
            throw new InvalidOperationException("The application details cannot be found in the database.");
        }

        // Create the claims-based identity that will be used by OpenIddict to generate tokens.
        ClaimsIdentity identity;

        if(authentication is not null)
        identity = new ClaimsIdentity(
            authentication?.Principal?.Claims,
            authenticationType: TokenValidationParameters.DefaultAuthenticationType,
            nameType: Claims.Name,
            roleType: Claims.Role);
        else identity = new ClaimsIdentity(
            authenticationType: TokenValidationParameters.DefaultAuthenticationType,
            nameType: Claims.Name,
            roleType: Claims.Role);

        // Add the claims that will be persisted in the tokens (use the client_id as the subject identifier).
        identity.SetClaim(Claims.Subject, await _applicationManager.GetClientIdAsync(application));
        identity.SetClaim(Claims.Name, await _applicationManager.GetDisplayNameAsync(application));

        identity.SetScopes(request.GetScopes());
        identity.SetResources(await _scopeManager.ListResourcesAsync(identity.GetScopes()).ToListAsync());
        identity.SetDestinations(GetDestination.GetDestinations);

        return Results.SignIn(new ClaimsPrincipal(identity), new(), OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }
}