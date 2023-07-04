using Microsoft.Extensions.Configuration;
using Travel.Aggregator.Exceptions;
using Travel.Aggregator.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IAuthorService, AuthorService>();
builder.Services.AddTransient<IBookService, BookService>();
builder.Services.AddTransient<IEditorialService, EditorialService>();

builder.Services.AddTransient<GrpcExceptionInterceptor>();

builder.Services.AddGrpcClient<AuthorTravelService.AuthorTravelServiceClient>(options => {
    var urlbase = configuration.GetValue("GRPC_URL", "http://docker.for.mac.localhost:5206");
    ArgumentNullException.ThrowIfNull(urlbase);
    options.Address = new Uri(urlbase);
}).AddInterceptor<GrpcExceptionInterceptor>();

builder.Services.AddGrpcClient<BookTravelService.BookTravelServiceClient>(options => {
    var urlbase = configuration.GetValue("GRPC_URL", "http://docker.for.mac.localhost:5206");
    ArgumentNullException.ThrowIfNull(urlbase);
    options.Address = new Uri(urlbase);
}).AddInterceptor<GrpcExceptionInterceptor>();

builder.Services.AddGrpcClient<EditorialTravelService.EditorialTravelServiceClient>(options => {
    var urlbase = configuration.GetValue("GRPC_URL", "http://docker.for.mac.localhost:5206");
    ArgumentNullException.ThrowIfNull(urlbase);
    options.Address = new Uri(urlbase);
}).AddInterceptor<GrpcExceptionInterceptor>();


builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
