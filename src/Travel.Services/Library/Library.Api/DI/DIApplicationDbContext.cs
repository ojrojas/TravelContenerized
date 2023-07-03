namespace Library.Api.DI;

public static class DIApplicationDbContext
{
	public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
	{
		var connection = configuration["ConnectionLibrary"];

        ArgumentNullException.ThrowIfNull(connection);
		services.AddDbContext<LibraryDbContext>(con => con.UseSqlServer(connection));
		return services;
	}
}

