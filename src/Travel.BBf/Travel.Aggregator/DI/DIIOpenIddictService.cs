namespace Travel.Aggregator.DI;

public static class DIIOpenIddictService
{
    public static IServiceCollection AddDIIOpenIddictService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOpenIddict()
            .AddValidation(options =>
            {
                // Note: the validation handler uses OpenID Connect discovery
                // to retrieve the issuer signing keys used to validate tokens.

                options.SetIssuer(configuration["IdentityUrl"]);
                options.AddAudiences("AggregatorApi");
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

