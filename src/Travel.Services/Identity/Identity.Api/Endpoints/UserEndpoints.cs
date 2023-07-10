namespace Identity.Api.Endpoints;

public static class UserEndpoints
{
    public static RouteGroupBuilder UserEndpointsGroup(this RouteGroupBuilder group)
    {
        group.MapPost("/createuser", [Authorize]
        async (ILoggerFactory factory, IApplicationUserService _service, [FromBody] CreateApplicationUserRequest request) =>
        {
            var logger = factory.CreateLogger("createuser");
            logger.LogInformation("Request create user");
            return await _service.CreateApplicationUserAsync(request, default);
        });

        group.MapGet("/getallusers", [Authorize]
        async (ILoggerFactory factory, IApplicationUserService _service) => {
            var logger = factory.CreateLogger("getallusers");
            logger.LogInformation("Request get all users");
            return await _service.GetAllUserApplicationsAsync(new(), default);
        });

        group.MapDelete("/deleteuser", [Authorize]
        async (ILoggerFactory factory, IApplicationUserService _service, [FromBody] DeleteApplicationUserRequest request) => {
            var logger = factory.CreateLogger("deleteuser");
            logger.LogInformation("Request delete user");
            return await _service.DeleteUserApplicationAsync(request, default);
        });

        group.MapGet("/getuserbyid/{Id}", [Authorize]
        async (ILoggerFactory factory, IApplicationUserService _service, [FromRoute] Guid Id) => {
            var logger = factory.CreateLogger("getuserbyid");
            logger.LogInformation("Request get user by id");
            return await _service.GetByIdUserApplicationsAsync(new GetByIdApplicationUserRequest { Id = Id }, default);
        });

        group.MapPatch("/updateuser", [Authorize]
        async (ILoggerFactory factory, IApplicationUserService _service, [FromBody] UpdateApplicationUserRequest request) => {
            var logger = factory.CreateLogger("updateuser");
            logger.LogInformation("Request update user");
            return await _service.UpdateApplicationUserAsync(request, default);
        });

        return group;
    }
}
