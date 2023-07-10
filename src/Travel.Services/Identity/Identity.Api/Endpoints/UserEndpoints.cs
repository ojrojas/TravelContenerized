namespace Identity.Api.Endpoints;

public static class UserEndpoints
{
    public static RouteGroupBuilder UserEndpointsGroup(this RouteGroupBuilder group)
    {
        group.MapPost("/createuser",  [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        async (ILoggerFactory _factory, IApplicationUserService _service, [FromBody] CreateApplicationUserRequest request) =>
        {
            var logger = _factory.CreateLogger("createuser");
            logger.LogInformation("Request create user");
            return await _service.CreateApplicationUserAsync(request, default);
        });
                                     
        group.MapGet("/getallusers",  [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        async (ILoggerFactory factory, IApplicationUserService _service) => {
            var logger = factory.CreateLogger("getallusers");
            logger.LogInformation("Request get all users");
            var users =  await _service.GetAllUserApplicationsAsync(new(), default);
            foreach(var user in users.ApplicationsUsers)
            {
                user.PasswordHash = "PRIVATED FIELD";
                user.SecurityStamp = "PRIVATE FIELD";
                user.ConcurrencyStamp = "PRIVATE FIELD";
            }

            return users;
        });

        group.MapDelete("/deleteuser",  [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        async (ILoggerFactory factory, IApplicationUserService _service, [FromBody] DeleteApplicationUserRequest request) => {
            var logger = factory.CreateLogger("deleteuser");
            logger.LogInformation("Request delete user");
            return await _service.DeleteUserApplicationAsync(request, default);
        });

        group.MapGet("/getuserbyid/{Id}",  [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        async (ILoggerFactory factory, IApplicationUserService _service, [FromRoute] Guid Id) => {
            var logger = factory.CreateLogger("getuserbyid");
            logger.LogInformation("Request get user by id");
            var user =  await _service.GetByIdUserApplicationsAsync(new GetByIdApplicationUserRequest { Id = Id }, default);
            if(user.ApplicationUser is not null)
            {
                user.ApplicationUser.PasswordHash = "PRIVATED FIELD";
                user.ApplicationUser.SecurityStamp = "PRIVATE FIELD";
                user.ApplicationUser.ConcurrencyStamp = "PRIVATE FIELD";
            }
        });

        group.MapPatch("/updateuser",  [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        async (ILoggerFactory factory, IApplicationUserService _service, [FromBody] UpdateApplicationUserRequest request) => {
            var logger = factory.CreateLogger("updateuser");
            logger.LogInformation("Request update user");
            return await _service.UpdateApplicationUserAsync(request, default);
        });

        return group;
    }
}
