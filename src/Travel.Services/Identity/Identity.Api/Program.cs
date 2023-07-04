using Identity.Api.Endpoints;
using Identity.Core.Helpers;
using Microsoft.AspNetCore.HttpOverrides;
using OpenIddict.Validation.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();
Log.Logger = CreateSerilogLogger();

var configuration = builder.Configuration;

builder.Services.AddDIApplicationDbContext(configuration);
// Add services to the container.
builder.Services.AddQuartz(conf => {
    conf.UseMicrosoftDependencyInjectionJobFactory();
    conf.UseSimpleTypeLoader();
    conf.UseInMemoryStore();
});

builder.Services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

builder.Services.AddDIOpenIddictApplication();
builder.Services.AddDIIdentityApplication();
builder.Services.AddDIServicesApplication();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.LoginPath = "/login";
    });

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

var app = builder.Build();

var urls = configuration["UrlsAllow"];
ArgumentNullException.ThrowIfNull(urls);
var clientUrls = TransformString.TransformStringtoDictionary(urls);

using var scope = app.Services.CreateScope();
var service = scope.ServiceProvider;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    var initializer = service.GetRequiredService<InitializerDbContext>();
    var context = service.GetRequiredService<IdentityTravelDbContext>();
    var _managerApplication = service.GetRequiredService<IOpenIddictApplicationManager>();
    var _managerScopes = service.GetRequiredService<IOpenIddictScopeManager>();

    await initializer.Run();
    var applied = context.Database.GetAppliedMigrations();

    await initializer.RunConfigurationDbContext(_managerApplication, _managerScopes, clientUrls);

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseForwardedHeaders();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapGroup("").AuthorizationEndpointsGroup();
app.MapGroup("").ApplicationUserEndpointsGroup();

app.MapGet("/api", [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
(ClaimsPrincipal user) => user.Identity!.Name);


app.Run();

static Serilog.ILogger CreateSerilogLogger() => new LoggerConfiguration()
        .MinimumLevel.Verbose()
        .Enrich.WithProperty("TravelServerApplication", typeof(Program).Namespace)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .CreateLogger();

