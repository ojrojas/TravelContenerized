namespace Library.Api.Endpoints;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UpdateEditorial: ControllerBase
{
    private readonly IEditorialService _service;
    private readonly ILogger<UpdateEditorial> _logger;

    public UpdateEditorial(IEditorialService service, ILogger<UpdateEditorial> logger)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpPatch]
    [Produces(typeof(UpdateEditorialResponse))]
    [SwaggerOperation(
       Summary = "Create editorial application",
       Description = "Create editorial application",
       OperationId = "editorial.createeditorials",
       Tags = new[] { "EditorialEndpoints" })]
    public async ValueTask<UpdateEditorialResponse> Update(UpdateEditorialRequest request, CancellationToken cancellationToken)
    {
        return await _service.UpdateEditorialAsync(request, cancellationToken);
    }
}