using System;
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

		services.AddTransient<IAuthorService, AuthorService>();
        services.AddTransient<IBookService, BookService>();
        services.AddTransient<IEditorialService, EditorialService>();

        return services;
	}
}

