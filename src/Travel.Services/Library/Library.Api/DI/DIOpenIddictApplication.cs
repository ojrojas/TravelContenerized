namespace Library.Api.DI;

public static class DIOpenIddictApplication
{
    public static IServiceCollection AddDIOpenIddictApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOpenIddict()
            .AddValidation(options =>
            {
                // Note: the validation handler uses OpenID Connect discovery
                // to retrieve the issuer signing keys used to validate tokens.

                options.SetIssuer(configuration["IdentityUrl"]);
                options.AddAudiences("LibraryApi");
                options.AddEncryptionKey(new SymmetricSecurityKey(
                    Convert.FromBase64String("QXBwbGljYXRpb25UcmF2ZWxTb2x1dGlvbnNJZGVudCs=")));

                // Register the System.Net.Http integration.
                options.UseSystemNetHttp();

                // Register the ASP.NET Core host.
                options.UseAspNetCore();
            });

        return services;
    }
}

