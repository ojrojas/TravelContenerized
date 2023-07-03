namespace Library.Api.Endpoints;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DeleteEditorial: ControllerBase
{
    private readonly IEditorialService _service;
    private readonly ILogger<DeleteEditorial> _logger;

    public DeleteEditorial(IEditorialService service, ILogger<DeleteEditorial> logger)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpDelete]
    [Produces(typeof(DeleteEditorialResponse))]
    [SwaggerOperation(
       Summary = "Delete editorial application",
       Description = "Delete editorial application",
       OperationId = "editorial.deleteeditorials",
       Tags = new[] { "EditorialEndpoints" })]
    public async ValueTask<DeleteEditorialResponse> Delete(DeleteEditorialRequest request, CancellationToken cancellationToken)
    {
        return await _service.DeleteEditorialAsync(request, cancellationToken);
    }
}