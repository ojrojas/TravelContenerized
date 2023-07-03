namespace Library.Api.Endpoints;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CreateBook: ControllerBase
{
    private readonly IBookService _service;
    private readonly ILogger<CreateBook> _logger;

    public CreateBook(IBookService service, ILogger<CreateBook> logger)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpPost]
    [Produces(typeof(CreateBookResponse))]
    [SwaggerOperation(
       Summary = "Create book application",
       Description = "Create book application",
       OperationId = "book.createbook",
       Tags = new[] { "BookEndpoints" })]
    public async ValueTask<CreateBookResponse> Create([FromBody] CreateBookRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Create book request...");
        return await _service.CreateBookAsync(request, cancellationToken);
    }
}