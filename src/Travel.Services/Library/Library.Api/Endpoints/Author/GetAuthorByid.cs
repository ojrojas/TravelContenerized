namespace Library.Api.Endpoints;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class GetAuthorById
{
    private readonly IAuthorService _service;
    private readonly ILogger<GetAuthorById> _logger;

    public GetAuthorById(IAuthorService service, ILogger<GetAuthorById> logger)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet("{Id}")]
    [Produces(typeof(GetAuthorByIdResponse))]
    [SwaggerOperation(
        Summary = "Get author by id application",
        Description = "Get author by id application",
        OperationId = "author.getauthorbyid",
        Tags = new[] { "AuthorEndpoints" })]
    public async ValueTask<GetAuthorByIdResponse> Get([FromRoute]GetAuthorByIdRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Get author by id author request...");
        return await _service.GetAuthorByIdAsync(request, cancellationToken);
    }
}