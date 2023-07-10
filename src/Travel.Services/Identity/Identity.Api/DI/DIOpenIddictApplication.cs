namespace Identity.Api.DI;

public static class DIOpenIddictApplication
{
    public static IServiceCollection AddDIOpenIddictApplication(this IServiceCollection services)
    {
        services.AddOpenIddict(config =>
        {
            config.AddCore(opt =>
            {
                opt.UseEntityFrameworkCore()
                .UseDbContext<IdentityTravelDbContext>();
            });

            config.AddServer(opt =>
            {
                // this implementation only need allow password and credentials flow
                opt.AllowClientCredentialsFlow()
                .AllowPasswordFlow()
                .AcceptAnonymousClients()
                .AllowAuthorizationCodeFlow()
                .RequireProofKeyForCodeExchange();

                opt.SetTokenEndpointUris("/connect/token")
                .SetAuthorizationEndpointUris("/connect/authorize");

                //// secretPhrase: ApplicationTravelSolutionsIdent +
                opt.AddEncryptionKey(new SymmetricSecurityKey(
                    Convert.FromBase64String("QXBwbGljYXRpb25UcmF2ZWxTb2x1dGlvbnNJZGVudCs=")));

                opt.AddSigningCertificate(Certificate.GetCert());
                opt.RegisterScopes(Scopes.Email, Scopes.Profile, Scopes.Roles, "api1", "aggregator");

                opt.UseAspNetCore()
                .DisableTransportSecurityRequirement()
                .EnableAuthorizationEndpointPassthrough()
                .EnableLogoutEndpointPassthrough()
                .EnableStatusCodePagesIntegration()
                .EnableTokenEndpointPassthrough();
            })
            .AddValidation(opt =>
            {
                opt.UseLocalServer();
                opt.UseAspNetCore();
            });

        });

        return services;
    }
}

