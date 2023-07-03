namespace Identity.Core.Data;

/// <summary>
/// Initializer db context
/// </summary>
public class InitializerDbContext
{
    /// <summary>
    /// Identity db context
    /// </summary>
    private readonly IdentityTravelDbContext _context;

    /// <summary>
    /// Password hasher
    /// </summary>
    private readonly IPasswordHasher<ApplicationUser> _passwordHasher = new PasswordHasher<ApplicationUser>();

    /// <summary>
    /// Initializer db context
    /// </summary>
    /// <param name="context">Context Application</param>
    /// <param name="context2">Context IdentityServer</param>
    public InitializerDbContext(IdentityTravelDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Run 
    /// </summary>
    /// <returns>Task completes</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public virtual async Task Run()
    {
#if DEBUG
        try
        {
            await _context.Database.EnsureDeletedAsync();
            await _context.Database.EnsureCreatedAsync();
            var usersApplications = _context.Users;
            if (!usersApplications.Any())
            {
                var userApplication = SeedIdentity.CreateApplicationUser();
                ArgumentNullException.ThrowIfNull(userApplication.PasswordHash);
                userApplication.PasswordHash = _passwordHasher.HashPassword(userApplication, userApplication.PasswordHash);
                await _context.Users.AddAsync(userApplication);
                await _context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.Message, ex);
        }

#endif
    }

    /// <summary>
    /// Run configuration db context
    /// </summary>
    /// <param name="clientsUrls">clients urls</param>
    /// <returns>Task completes</returns>
    /// <exception cref="InvalidOperationException">Invalid operation</exception>
    public virtual async Task RunConfigurationDbContext(IOpenIddictApplicationManager manager, IOpenIddictScopeManager _scopeManager, Dictionary<string, string> clientsUrls)
    {
        try
        {
            if (await manager.FindByClientIdAsync("libraryapiswaggerui") is null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "libraryapiswaggerui",
                    DisplayName = "Library Swagger UI",
                    ClientSecret = "libraryapiswaggerui-secret",

                    RedirectUris = { new Uri("https://oauth.pstmn.io/v1/callback") },
                    Permissions = {
                        Permissions.Endpoints.Token,
                        Permissions.GrantTypes.ClientCredentials,
                        Permissions.GrantTypes.AuthorizationCode,
                        Permissions.Endpoints.Authorization,
                        Permissions.ResponseTypes.Code,
                        Permissions.Scopes.Email,
                        Permissions.Scopes.Profile,
                        Permissions.Scopes.Roles,
                        Permissions.Prefixes.Scope + "api1",
                    },
                    PostLogoutRedirectUris = { new Uri($"{clientsUrls["library"]}/swagger") },
                });
            }

            if (await manager.FindByClientIdAsync("aggregatorswaggerui") is null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "aggregatorswaggerui",
                    DisplayName = "Aggregator Swagger UI",
                    ClientSecret = "aggregatorswaggerui-secret",
                    Permissions = {
                        Permissions.Endpoints.Authorization,
                        Permissions.Endpoints.Token,
                        Permissions.GrantTypes.ClientCredentials,
                        Permissions.ResponseTypes.Code,
                        Permissions.Scopes.Email,
                        Permissions.Scopes.Profile,
                        Permissions.Scopes.Roles,
                        Permissions.Prefixes.Scope + "AggregatorScope",
                        Permissions.Prefixes.Scope + "LibraryScope"
                    }
                });
            }

            if (await _scopeManager.FindByNameAsync("aggregator") is null)
            {
                await _scopeManager.CreateAsync(new OpenIddictScopeDescriptor
                {
                    Name = "AggregatorScope",
                    DisplayName = "Aggregator Api",
                    Resources = {
                        "resource_aggregator"
                    }
                });
            }

            if (await _scopeManager.FindByNameAsync("library") is null)
            {
                await _scopeManager.CreateAsync(new OpenIddictScopeDescriptor
                {
                    Name = "LibraryScope",
                    DisplayName = "Library Api",
                    Resources = {
                        "resource_library"
                    }
                });
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.Message, ex);
        }
    }
}

