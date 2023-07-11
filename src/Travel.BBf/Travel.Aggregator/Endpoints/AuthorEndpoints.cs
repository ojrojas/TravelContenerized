
namespace Travel.Aggregator.Endpoints;

public static class AuthorEndpoints
{
    public static RouteGroupBuilder AuthorEndpointGroup(this RouteGroupBuilder group)
    {
        group.MapPost("CreateAuthor", ([FromServices]ILoggerFactory factory, [FromServices] IAuthorService _service, [FromServices] CallOptions callOptions,[FromBody] CreateAuthorRequest request) => {
            var logger = factory.CreateLogger("CreateAuthor");
            logger.LogInformation("Create author request...");

            return _service.CreateAuthorAsync(request, callOptions);
        });

        group.MapGet("GetAllAuthors", ([FromServices] ILoggerFactory factory, [FromServices] IAuthorService _service, [FromServices] CallOptions callOptions) => {
            var logger = factory.CreateLogger("GetAllAuthor");
            logger.LogInformation("Get all authors request...");

            return _service.CreateAuthorAsync(new (), callOptions);
        });

        group.MapDelete("DeleteAuthor/{id}", ([FromServices] ILoggerFactory factory, [FromServices] IAuthorService _service, [FromServices] CallOptions callOptions, [FromRoute]string id ) => {
            var logger = factory.CreateLogger("DeleteAuthor");
            logger.LogInformation("Delete author request...");

            return _service.DeleteAuthorAsync(new DeleteAuthorRequest { Id = id }, callOptions);
        });

        group.MapGet("GetByIdAuthor/{id}", ([FromServices] ILoggerFactory factory, [FromServices] IAuthorService _service, [FromServices] CallOptions callOptions, [FromRoute]string  id) => {
            var logger = factory.CreateLogger("Get by id author request...");
            logger.LogInformation("Get by id request...");

            return _service.GetAuthorByIdAsync(new GetAuthorByIdRequest {  Id = id}, callOptions);
        });

        group.MapPatch("UpdateAuthor", ([FromServices] ILoggerFactory factory, [FromServices] IAuthorService _service, [FromServices] CallOptions callOptions,[FromBody] UpdateAuthorRequest request) => {
            var logger = factory.CreateLogger("UpdateAuthor");
            logger.LogInformation("Update author request...");

            return _service.UpdateAuthorAsync(request, callOptions);
        });

        return group;
    }
}

