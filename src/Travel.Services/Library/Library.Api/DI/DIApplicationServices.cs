using Travel.Repository.Data;

namespace Library.Api.DI;

public static class DIApplicationServices
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddTransient(typeof(GenericRepository<>));
		services.AddTransient(typeof(AuthorRepository));
        services.AddTransient(typeof(BookRepository));
        services.AddTransient(typeof(EditorialRepository));

		services.AddAutoMapper(typeof(Program));

        return services;
	}
}

