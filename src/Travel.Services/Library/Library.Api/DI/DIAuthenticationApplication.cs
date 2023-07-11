namespace Library.Api.DI;

public static class DIAuthenticationApplication
{
	public static IServiceCollection AddDIAuthenticationApplication(this IServiceCollection services)
	{
            services.AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
            services.AddAuthorization();

            return services;
	}
}

