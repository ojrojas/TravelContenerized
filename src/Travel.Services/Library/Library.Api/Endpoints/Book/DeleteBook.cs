namespace Library.Api.Endpoints;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DeleteBook: ControllerBase
{
    private readonly IBookService _service;
    private readonly ILogger<DeleteBook> _logger;

    public DeleteBook(IBookService service, ILogger<DeleteBook> logger)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpDelete("{Id}")]
    [Produces(typeof(DeleteBookResponse))]
    [SwaggerOperation(
       Summary = "Create book application",
       Description = "Create book application",
       OperationId = "book.createbook",
       Tags = new[] { "BookEndpoints" })]
    public async ValueTask<DeleteBookResponse> Delete([FromRoute]DeleteBookRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Create book request...");
        return await _service.DeleteBookAsync(request, cancellationToken);
    }
}