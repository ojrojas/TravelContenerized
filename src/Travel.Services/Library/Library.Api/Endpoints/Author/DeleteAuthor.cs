namespace Library.Api.Endpoints;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DeleteAuthor: ControllerBase
{
    private readonly IAuthorService _service;
    private readonly ILogger<DeleteAuthor> _logger;

    public DeleteAuthor(IAuthorService service, ILogger<DeleteAuthor> logger)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpDelete("{Id}")]
    [Produces(typeof(DeleteAuthorResponse))]
    [SwaggerOperation(
       Summary = "Delete author application",
       Description = "Delete author application",
       OperationId = "author.deleteauthor",
       Tags = new[] { "AuthorEndpoints" })]
    public async ValueTask<DeleteAuthorResponse> Delete([FromRoute]DeleteAuthorRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Delete request...");
        return await _service.DeleteAuthorAsync(request, cancellationToken);
    }
}