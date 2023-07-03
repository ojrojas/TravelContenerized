namespace Identity.Api.DI;

public static class DIServicesApplication
{
	public static IServiceCollection AddDIServicesApplication(this IServiceCollection services)
	{
		services.AddTransient(typeof(GenericRepository<>));
		services.AddTransient(typeof(ApplicationUserRepository));
		services.AddTransient<IApplicationUserService,ApplicationUserService>();

		return services;
	}
}

