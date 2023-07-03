namespace Library.Api.Endpoints;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UpdateBook: ControllerBase
{
    private readonly IBookService _service;
    private readonly ILogger<CreateBook> _logger;

    public UpdateBook(IBookService service, ILogger<CreateBook> logger)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpPatch]
    [Produces(typeof(UpdateBookResponse))]
    [SwaggerOperation(
       Summary = "Create book application",
       Description = "Create book application",
       OperationId = "book.createbook",
       Tags = new[] { "BookEndpoints" })]
    public async ValueTask<UpdateBookResponse> Update([FromBody]UpdateBookRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Create book request...");
        return await _service.UpdateBookAsync(request, cancellationToken);
    }
}