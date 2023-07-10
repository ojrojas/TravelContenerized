using Microsoft.AspNetCore.Identity;

namespace Identity.Core.Services;

/// <summary>
/// Application User service
/// </summary>
public class ApplicationUserService : IApplicationUserService
{
    /// <summary>
    /// Logger application services
    /// </summary>
    private readonly ILogger<ApplicationUserService> _logger;
    /// <summary>
    /// Application repository
    /// </summary>
    private readonly ApplicationUserRepository _repository;
    /// <summary>
    /// UserManager
    /// </summary>
    private readonly UserManager<ApplicationUser> _userManager;
    /// <summary>
    /// Sign in manager
    /// </summary>
    private readonly SignInManager<ApplicationUser> _signInManager;

    /// <summary>
    /// Application usermanager
    /// </summary>
    private readonly IOpenIddictApplicationManager _applicationManager;

    /// <summary>
    /// Authorization manager
    /// </summary>
    private readonly IOpenIddictAuthorizationManager _authorizationManager;

    /// <summary>
    /// Scope manager open iddict
    /// </summary>
    private readonly IOpenIddictScopeManager _scopeManager;

    /// <summary>
    /// Password hasher
    /// </summary>
    private readonly IPasswordHasher<ApplicationUser> _passwordHasher = new PasswordHasher<ApplicationUser>();

    /// <summary>
    /// Constructor User services
    /// </summary>
    /// <param name="logger">Logger application</param>
    /// <param name="repository">Repository user application</param>
    /// <param name="userManager">User manager application</param>
    /// <param name="signInManager">Signin application</param>
    /// <param name="applicationManager">Application manager open iddict</param>
    /// <param name="authorizationManager">Authorization manager open iddict</param>
    /// <param name="scopeManager">Scope manager</param>
    /// <exception cref="ArgumentNullException">Exception argument if exists</exception>
    public ApplicationUserService(ILogger<ApplicationUserService> logger,
                                  ApplicationUserRepository repository,
                                  UserManager<ApplicationUser> userManager,
                                  SignInManager<ApplicationUser> signInManager,
                                  IOpenIddictApplicationManager applicationManager,
                                  IOpenIddictAuthorizationManager authorizationManager,
                                  IOpenIddictScopeManager scopeManager)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        _applicationManager = applicationManager ?? throw new ArgumentNullException(nameof(applicationManager));
        _authorizationManager = authorizationManager ?? throw new ArgumentNullException(nameof(authorizationManager));
        _scopeManager = scopeManager ?? throw new ArgumentNullException(nameof(scopeManager));
    }

    public async ValueTask<CreateApplicationUserResponse> CreateApplicationUserAsync(CreateApplicationUserRequest request, CancellationToken cancellationToken)
    {
        var response = new CreateApplicationUserResponse(request.CorrelationId());
        if (request.ApplicationUser is null) throw new ArgumentNullException(nameof(request.ApplicationUser));
        ArgumentNullException.ThrowIfNull(request.ApplicationUser.PasswordHash);
        ArgumentNullException.ThrowIfNull(request.ApplicationUser.UserName);
        request.ApplicationUser.UserName = request.ApplicationUser.UserName.ToLowerInvariant();
        request.ApplicationUser.PasswordHash = _passwordHasher.HashPassword(request.ApplicationUser, request.ApplicationUser.PasswordHash);
        _logger.LogInformation($"Create user application {JsonSerializer.Serialize(request.ApplicationUser)}");
        var created = await _repository.CreateAsync(request.ApplicationUser, cancellationToken);
        response.ApplicationUserCreated = created;

        return response;
    }

    public async ValueTask<UpdateApplicationUserResponse> UpdateApplicationUserAsync(UpdateApplicationUserRequest request, CancellationToken cancellationToken)
    {
        var response = new UpdateApplicationUserResponse(request.CorrelationId());
        if (request.ApplicationUser is null) throw new ArgumentNullException(nameof(request.ApplicationUser));
        ArgumentNullException.ThrowIfNull(request.ApplicationUser.PasswordHash);
        ArgumentNullException.ThrowIfNull(request.ApplicationUser.UserName);
        request.ApplicationUser.UserName = request.ApplicationUser.UserName.ToLowerInvariant();
        request.ApplicationUser.PasswordHash = _passwordHasher.HashPassword(request.ApplicationUser, request.ApplicationUser.PasswordHash);
        _logger.LogInformation($"Create user application {JsonSerializer.Serialize(request.ApplicationUser)}");
        var updated = await _repository.UpdateAsync(request.ApplicationUser, cancellationToken);
        response.ApplicationUserUpdated = updated;

        return response;
    }

    public async ValueTask<DeleteApplicationUserResponse> DeleteUserApplicationAsync(DeleteApplicationUserRequest request, CancellationToken cancellationToken)
    {
        DeleteApplicationUserResponse response = new(request.CorrelationId());
        if (request.Id.Equals(Guid.Empty)) throw new ArgumentNullException(nameof(request.Id));
        ApplicationUser userApplication = await _repository.GetByIdAsync(request.Id, cancellationToken);
        response.ApplicationUserDeleted = await _repository.DeleteAsync(userApplication, cancellationToken);
        return response;
    }

    public async ValueTask<GetAllApplicationUserResponse> GetAllUserApplicationsAsync(GetAllApplicationUserRequest request, CancellationToken cancellationToken)
    {
        GetAllApplicationUserResponse response = new(request.CorrelationId());
        _logger.LogInformation($"Get all user applications request");
        response.ApplicationsUsers = await _repository.ListAsync(cancellationToken);
        return response;
    }

    public async ValueTask<GetByIdApplicationUserResponse> GetByIdUserApplicationsAsync(GetByIdApplicationUserRequest request, CancellationToken cancellationToken)
    {
        GetByIdApplicationUserResponse response = new(request.CorrelationId());
        _logger.LogInformation($"Get by id user applications request");
        response.ApplicationUser = await _repository.GetByIdAsync(request.Id, cancellationToken);
        return response;
    }

    public async ValueTask<IResult> LoginAsync(LoginApplicationUserRequest request, CancellationToken cancellationToken)
    {
        try
        {
            LoginApplicationUserResponse response = new(request.CorrelationId())
            {
                Token = "Do not get token response"
            };

            _logger.LogInformation($"Encrypt password and get user by login");
            request.UserName = request.UserName.ToLowerInvariant();
            ApplicationUserSpecification specification = new(request.UserName);
            var result = await _repository.FirstOrDefaultAsync(specification, cancellationToken);
            if (result is not null)
            {
                var token = await _userManager.CheckPasswordAsync(result, request.Password);
                if (token)
                {
                    // Retrieve the application details from the database.
                    var application = await _applicationManager.FindByClientIdAsync(request.ClientId) ??
                        throw new InvalidOperationException("Details concerning the calling client application cannot be found.");

                    // Retrieve the permanent authorizations associated with the user and the calling client application.
                    var authorizations = await _authorizationManager.FindAsync(
                        subject: await _userManager.GetUserIdAsync(result),
                        client: await _applicationManager.GetIdAsync(application, cancellationToken),
                        status: Statuses.Valid,
                        type: AuthorizationTypes.Permanent,
                        scopes: request.Scopes.ToImmutableArray(),
                        cancellationToken).ToListExtensionsAsync();

                    // Create the claims-based identity that will be used by OpenIddict to generate tokens.
                    var identity = new ClaimsIdentity(
                        authenticationType: TokenValidationParameters.DefaultAuthenticationType,
                        nameType: Claims.Name,
                        roleType: Claims.Role);

                    // Add the claims that will be persisted in the tokens.
                    identity.SetClaim(Claims.Subject, await _userManager.GetUserIdAsync(result))
                            .SetClaim(Claims.Email, await _userManager.GetEmailAsync(result))
                            .SetClaim(Claims.Name, await _userManager.GetUserNameAsync(result))
                            .SetClaims(Claims.Role, (await _userManager.GetRolesAsync(result)).ToImmutableArray());

                    // Note: in this sample, the granted scopes match the requested scope
                    // but you may want to allow the user to uncheck specific scopes.
                    // For that, simply restrict the list of scopes before calling SetScopes.
                    identity.SetScopes(request.Scopes);
                    identity.SetResources(await _scopeManager.ListResourcesAsync(identity.GetScopes()).ToListExtensionsAsync());

                    // Automatically create a permanent authorization to avoid requiring explicit consent
                    // for future authorization or token requests containing the same scopes.
                    var authorization = authorizations.LastOrDefault();
                    authorization ??= await _authorizationManager.CreateAsync(
                        identity: identity,
                        subject: await _userManager.GetUserIdAsync(result),
                        client: await _applicationManager.GetIdAsync(application),
                        type: AuthorizationTypes.Permanent,
                        scopes: identity.GetScopes());

                    identity.SetAuthorizationId(await _authorizationManager.GetIdAsync(authorization));
                    identity.SetDestinations(GetDestination.GetDestinations);

                    // Returning a SignInResult will ask OpenIddict to issue the appropriate access/identity tokens.
                    response.ActionResult = Results.SignIn(new ClaimsPrincipal(identity), new(), OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
                    response.Token = "Signin successful";
                }else
                {
                    response.ActionResult = Results.Ok(new object[] { response.Token});
                }
            }

           
            return response.ActionResult;

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}