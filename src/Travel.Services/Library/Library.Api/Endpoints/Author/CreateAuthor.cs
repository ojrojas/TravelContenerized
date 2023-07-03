namespace Library.Api.Endpoints;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CreateAuthor : ControllerBase
{
    private readonly IAuthorService _service;
    private readonly ILogger<CreateAuthor> _logger;

    public CreateAuthor(IAuthorService service, ILogger<CreateAuthor> logger)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpPost]
    [Produces(typeof(CreateAuthorResponse))]
    [SwaggerOperation(
        Summary = "Create author application",
        Description = "Create author application",
        OperationId = "author.createauthor",
        Tags = new[] { "AuthorEndpoints" })]
    public async ValueTask<CreateAuthorResponse> Create([FromBody] CreateAuthorRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Create author request...");
        return await _service.CreateAuthorAsync(request, cancellationToken);
    }
}