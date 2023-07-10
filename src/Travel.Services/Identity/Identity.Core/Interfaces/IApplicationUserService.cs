namespace Identity.Core.Interfaces;

/// <summary>
/// Application user services
/// </summary>
public interface IApplicationUserService
{
    /// <summary>
    /// Create application user
    /// </summary>
    /// <param name="request">Request info</param>
    /// <param name="cancellationToken">Cancellation token request</param>
    /// <returns>Created application user</returns>
    ValueTask<CreateApplicationUserResponse> CreateApplicationUserAsync(CreateApplicationUserRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Delete application user
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="cancellationToken">Cancellation token request</param>
    /// <returns>Application application user</returns>
    ValueTask<DeleteApplicationUserResponse> DeleteUserApplicationAsync(DeleteApplicationUserRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Get all application user 
    /// </summary>
    /// <param name="request">Request info</param>
    /// <param name="cancellationToken">Cancellation token request</param>
    /// <returns>Get all application users</returns>
    ValueTask<GetAllApplicationUserResponse> GetAllUserApplicationsAsync(GetAllApplicationUserRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Get by id application user
    /// </summary>
    /// <param name="request">Request info id</param>
    /// <param name="cancellationToken">Cancellation token request</param>
    /// <returns>Get by id user application</returns>
    ValueTask<GetByIdApplicationUserResponse> GetByIdUserApplicationsAsync(GetByIdApplicationUserRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Login application user
    /// </summary>
    /// <param name="request">Request info user, pass</param>
    /// <param name="cancellationToken">Cancellation token request</param>
    /// <returns>Login application token</returns>
    ValueTask<IResult> LoginAsync(LoginApplicationUserRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Update application user 
    /// </summary>
    /// <param name="request">Request info</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Updated application user</returns>
    ValueTask<UpdateApplicationUserResponse> UpdateApplicationUserAsync(UpdateApplicationUserRequest request, CancellationToken cancellationToken);
}