namespace Travel.Aggregator.DI
{
    public static class DIGrpcClientsApplication
	{
		public static IServiceCollection AddDIGrpcClientsApplication(this IServiceCollection services, IConfiguration configuration)
		{
            services.AddGrpcClient<AuthorTravelService.AuthorTravelServiceClient>(options => {
                var urlbase = configuration.GetValue("GRPC_URL", "http://docker.for.mac.localhost:5206");
                ArgumentNullException.ThrowIfNull(urlbase);
                options.Address = new Uri(urlbase);
            }).AddInterceptor<GrpcExceptionInterceptor>();

            services.AddGrpcClient<BookTravelService.BookTravelServiceClient>(options => {
                var urlbase = configuration.GetValue("GRPC_URL", "http://docker.for.mac.localhost:5206");
                ArgumentNullException.ThrowIfNull(urlbase);
                options.Address = new Uri(urlbase);
            }).AddInterceptor<GrpcExceptionInterceptor>();

            services.AddGrpcClient<EditorialTravelService.EditorialTravelServiceClient>(options => {
                var urlbase = configuration.GetValue("GRPC_URL", "http://docker.for.mac.localhost:5206");
                ArgumentNullException.ThrowIfNull(urlbase);
                options.Address = new Uri(urlbase);
            }).AddInterceptor<GrpcExceptionInterceptor>();

            return services;
		}
	}
}

