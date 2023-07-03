namespace Library.Api.Endpoints;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class GetAllEditorials: ControllerBase
{
	private readonly IEditorialService _service;
	private readonly ILogger<GetAllEditorials> _logger;

    public GetAllEditorials(IEditorialService service, ILogger<GetAllEditorials> logger)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    [Produces(typeof(ListEditorialResponse))]
    [SwaggerOperation(
       Summary = "Get all editorial application",
       Description = "Get all editorial application",
       OperationId = "editorial.getalleditorials",
       Tags = new[] { "EditorialEndpoints" })]
    public async ValueTask<ListEditorialResponse> GetAll(CancellationToken cancellationToken)
    {
        return await _service.GetAllEditorialsAsync(new(), cancellationToken);
    }
}