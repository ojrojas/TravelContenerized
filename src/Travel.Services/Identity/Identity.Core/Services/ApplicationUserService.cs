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
    /// Password hasher
    /// </summary>
    private readonly IPasswordHasher<ApplicationUser> _passwordHasher = new PasswordHasher<ApplicationUser>();

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger">Logger application</param>
    /// <param name="repository">Repository user application</param>
    /// <param name="userManager">User manager</param>
    /// <param name="signInManager">Sign in manager</param>
    /// <exception cref="ArgumentNullException">Argument null exception</exception>
    public ApplicationUserService(ILogger<ApplicationUserService> logger,
                                  ApplicationUserRepository repository,
                                  UserManager<ApplicationUser> userManager,
                                  SignInManager<ApplicationUser> signInManager)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
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

    public async ValueTask<LoginApplicationUserResponse> LoginAsync(LoginApplicationUserRequest request, CancellationToken cancellationToken)
    {
        try
        {
            LoginApplicationUserResponse response = new(request.CorrelationId());
            response.Token = "Do not get token response";
            _logger.LogInformation($"Encrypt password and get user by login");
            request.UserName = request.UserName.ToLowerInvariant();
            ApplicationUserSpecification specification = new(request.UserName);
            var result = await _repository.FirstOrDefaultAsync(specification, cancellationToken);
            if (result is not null)
            {
                var token = await _userManager.CheckPasswordAsync(result, request.Password);
                if (token)
                {
                    await _signInManager.SignInAsync(result, true);
                    response.Token = "Signin successful";
                }
            }

            return response;

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}