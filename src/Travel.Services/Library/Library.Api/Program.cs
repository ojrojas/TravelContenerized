using HealthChecks.UI.Client;
using Library.Api.DI;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.IdentityModel.Logging;
using Serilog;
using Travel.Commons.Services;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddApplicationDbContext(configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IIdentityService, IdentityService>();
builder.Services.AddSwaggerApplication(configuration);
builder.Services.AddJwtApplication(configuration);

builder.Services.AddApplicationServices(configuration);

//builder.WebHost.ConfigureKestrel(opt =>
//{
//    opt.Listen(System.Net.IPAddress.Any, configuration.GetValue("PORT", 443), listenOptions =>
//    {
//        listenOptions.Protocols = HttpProtocols.Http2;
//    });
//});

builder.Services.AddHealthChecks();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
        .SetIsOriginAllowed((host) => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.OAuthClientId("libraryapiswaggerui");
        options.OAuthAppName("Library Swagger UI");
    });

    IdentityModelEventSource.ShowPII = true;
}

app.UseRouting();
app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health",
    new HealthCheckOptions
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.MapHealthChecks("/liveness",
    new HealthCheckOptions
    {
        Predicate = response => response.Name.Contains("self")
    });

app.Run();


static Serilog.ILogger CreateSerilogLogger() => new LoggerConfiguration()
        .MinimumLevel.Verbose()
        .Enrich.WithProperty("ApplicationContext", typeof(Program).Namespace)
        .Enrich.FromLogContext()
        .WriteTo.Console(
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
        .CreateLogger();