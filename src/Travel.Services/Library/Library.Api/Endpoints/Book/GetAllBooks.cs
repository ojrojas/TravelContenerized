namespace Library.Api.Endpoints;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class GetAllBooks
{
    private readonly IBookService _service;
    private readonly ILogger<GetAllBooks> _logger;

    public GetAllBooks(IBookService service, ILogger<GetAllBooks> logger)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    [Produces(typeof(ListBooksResponse))]
    [SwaggerOperation(
       Summary = "Get all books application",
       Description = "Get all books application",
       OperationId = "book.getallbooks",
       Tags = new[] { "BookEndpoints" })]
    public async ValueTask<ListBooksResponse> GetAll(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Update request...");
        return await _service.GetAllBooksAsync(new(), cancellationToken);
    }
}