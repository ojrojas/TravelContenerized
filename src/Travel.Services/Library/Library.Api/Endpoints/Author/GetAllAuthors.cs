namespace Library.Api.Endpoints;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class GetAllAuthors: ControllerBase
{
    private readonly IAuthorService _service;
    private readonly ILogger<CreateAuthor> _logger;

    public GetAllAuthors(IAuthorService service, ILogger<CreateAuthor> logger)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    [Produces(typeof(ListAuthorResponse))]
    [SwaggerOperation(
        Summary = "Get all authors application",
        Description = "Get all authors application",
        OperationId = "author.getallauthors",
        Tags = new[] { "AuthorEndpoints" })]
    public async ValueTask<ListAuthorResponse> GetAll(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Get all authors request...");
        return await _service.GetAllAuthorsAsync(new ListAuthorRequest(), cancellationToken);
    }
}