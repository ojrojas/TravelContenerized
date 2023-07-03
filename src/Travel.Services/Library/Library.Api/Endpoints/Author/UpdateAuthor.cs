namespace Library.Api.Endpoints;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UpdateAuthor: ControllerBase
{
    private readonly IBookService _service;
    private readonly ILogger<UpdateAuthor> _logger;

    public UpdateAuthor(IBookService service, ILogger<UpdateAuthor> logger)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpPatch]
    [Produces(typeof(UpdateBookResponse))]
    [SwaggerOperation(
       Summary = "Update author application",
       Description = "Update author application",
       OperationId = "author.updateauthor",
       Tags = new[] { "AuthorEndpoints" })]
    public async ValueTask<UpdateBookResponse> Update([FromBody] UpdateBookRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Update request...");
        return await _service.UpdateBookAsync(request, cancellationToken);
    }
}