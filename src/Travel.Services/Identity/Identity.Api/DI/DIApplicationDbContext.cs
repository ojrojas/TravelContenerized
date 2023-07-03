namespace Identity.Api.DI;

public static class DIApplicationDbContext
{
    public static IServiceCollection AddDIApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IdentityTravelDbContext>(opt =>
        {
            opt.UseNpgsql(configuration["ConnectionIdentity"]);
            opt.UseOpenIddict();
        });

        services.AddTransient<InitializerDbContext>();

        return services;
    }
}

