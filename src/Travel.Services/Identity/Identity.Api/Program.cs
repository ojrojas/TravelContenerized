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

builder.Services.AddAuthentication(option =>
{
    option.DefaultScheme = Schemes.Bearer;
    option.DefaultChallengeScheme = Schemes.Bearer;
});

var urls = configuration["UrlsAllow"];
ArgumentNullException.ThrowIfNull(urls);
var clientUrls = TransformString.TransformStringtoDictionary(urls);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "IdentityCorsPolicy",
    builder => builder.WithOrigins(clientUrls.Select(x=> x.Value).ToArray()).AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

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

app.UseCors("IdentityCorsPolicy");

app.UseForwardedHeaders();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapGroup("").ApplicationUserEndpointsGroup();

app.MapGet("/api", [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
(ClaimsPrincipal user) => user.Identity!.Name);
app.MapGroup("").UserEndpointsGroup();

app.Run();

static Serilog.ILogger CreateSerilogLogger() => new LoggerConfiguration()
        .MinimumLevel.Verbose()
        .Enrich.WithProperty("TravelServerApplication", typeof(Program).Namespace)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .CreateLogger();

