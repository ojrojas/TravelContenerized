namespace Travel.Aggregator.DI;

public static class DIApplicationServices
{
	public static IServiceCollection AddDIApplicationServices(this IServiceCollection services)
	{
        services.AddTransient<IAuthorService, AuthorService>();
        services.AddTransient<IBookService, BookService>();
        services.AddTransient<IEditorialService, EditorialService>();

        services.AddTransient<GrpcExceptionInterceptor>();

        return services;
	}
}

