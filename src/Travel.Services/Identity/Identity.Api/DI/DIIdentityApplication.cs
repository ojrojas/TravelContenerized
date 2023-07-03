namespace Identity.Api.DI;

public static class DIIdentityApplication
{
    public static IServiceCollection AddDIIdentityApplication(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<IdentityTravelDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}

