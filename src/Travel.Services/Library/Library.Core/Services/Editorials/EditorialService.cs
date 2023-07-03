namespace Library.Core.Services;

public class EditorialService : IEditorialService
{
    private readonly EditorialRepository _repository;
    private readonly ILogger<EditorialService> _logger;

    public EditorialService(EditorialRepository repository, ILogger<EditorialService> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async ValueTask<CreateEditorialResponse> CreateEditorialAsync(CreateEditorialRequest request, CancellationToken cancellationToken)
    {
        CreateEditorialResponse response = new(request.CorrelationId());
        if (request.Editorial is null) throw new ArgumentNullException(nameof(request));
        _logger.LogInformation($"Create author {JsonSerializer.Serialize(request.Editorial)}");
        response.EditorialCreated = await _repository.CreateAsync(request.Editorial, cancellationToken);
        return response;
    }

    public async ValueTask<DeleteEditorialResponse> DeleteEditorialAsync(DeleteEditorialRequest request, CancellationToken cancellationToken)
    {
        DeleteEditorialResponse response = new(request.CorrelationId());
        if (request.Id.Equals(Guid.Empty)) throw new ArgumentNullException(nameof(request));
        _logger.LogInformation($"Create author {JsonSerializer.Serialize(request.Id)}");
        var author = await _repository.GetByIdAsync(request.Id, cancellationToken);
        response.EditorialDeleted = await _repository.DeleteAsync(author, cancellationToken);
        return response;
    }

    public async ValueTask<UpdateEditorialResponse> UpdateEditorialAsync(UpdateEditorialRequest request, CancellationToken cancellationToken)
    {
        UpdateEditorialResponse response = new(request.CorrelationId());
        if (request.Editorial is null) throw new ArgumentNullException(nameof(request));
        _logger.LogInformation($"Create author {JsonSerializer.Serialize(request.Editorial)}");
        response.EditorialUpdated = await _repository.UpdateAsync(request.Editorial, cancellationToken);
        return response;
    }

    public async ValueTask<ListEditorialResponse> GetAllEditorialsAsync(ListEditorialRequest request, CancellationToken cancellationToken)
    {
        ListEditorialResponse response = new(request.CorrelationId());
        if (request is null) throw new ArgumentNullException(nameof(request));
        _logger.LogInformation($"Create author {JsonSerializer.Serialize(request)}");
        response.Editorials = await _repository.ListAsync(cancellationToken);
        return response;
    }
}
