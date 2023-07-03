namespace Library.Api.Endpoints;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CreateEditorial: ControllerBase
{
    private readonly IEditorialService _service;
    private readonly ILogger<CreateEditorial> _logger;

    public CreateEditorial(IEditorialService service, ILogger<CreateEditorial> logger)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpPost]
    [Produces(typeof(CreateEditorialResponse))]
    [SwaggerOperation(
       Summary = "Create editorial application",
       Description = "Create editorial application",
       OperationId = "editorial.createeditorials",
       Tags = new[] { "EditorialEndpoints" })]
    public async ValueTask<CreateEditorialResponse> Create(CreateEditorialRequest request,CancellationToken cancellationToken)
    {
        return await _service.CreateEditorialAsync(request, cancellationToken);
    }
}