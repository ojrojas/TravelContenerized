namespace Library.Core.Services;

public class AuthorService : IAuthorService
{
    private readonly AuthorRepository _repository;
    private readonly ILogger<AuthorService> _logger;

    public AuthorService(AuthorRepository repository, ILogger<AuthorService> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async ValueTask<CreateAuthorResponse> CreateAuthorAsync(CreateAuthorRequest request, CancellationToken cancellationToken)
    {
        CreateAuthorResponse response = new(request.CorrelationId());
        if (request.Author is null) throw new ArgumentNullException(nameof(request));
        _logger.LogInformation($"Create author {(request.Author)}");
        response.AuthorCreated = await _repository.CreateAsync(request.Author, cancellationToken);
        return response;
    }

    public async ValueTask<DeleteAuthorResponse> DeleteAuthorAsync(DeleteAuthorRequest request, CancellationToken cancellationToken)
    {
        DeleteAuthorResponse response = new(request.CorrelationId());
        if (request.Id.Equals(Guid.Empty)) throw new ArgumentNullException(nameof(request));
        _logger.LogInformation($"Create author {(request.Id)}");
        var author = await _repository.GetByIdAsync(request.Id, cancellationToken);
        response.AuthorDeleted = await _repository.DeleteAsync(author, cancellationToken);
        return response;
    }

    public async ValueTask<UpdateAuthorResponse> UpdateAuthorAsync(UpdateAuthorRequest request, CancellationToken cancellationToken)
    {
        UpdateAuthorResponse response = new(request.CorrelationId());
        if (request.Author is null) throw new ArgumentNullException(nameof(request));
        _logger.LogInformation($"Create author {(request.Author)}");
        response.AuthorUpdated = await _repository.UpdateAsync(request.Author, cancellationToken);
        return response;
    }

    public async ValueTask<ListAuthorResponse> GetAllAuthorsAsync(ListAuthorRequest request, CancellationToken cancellationToken)
    {
        ListAuthorResponse response = new(request.CorrelationId());
        if (request is null) throw new ArgumentNullException(nameof(request));
        _logger.LogInformation($"Create author {(request)}");
        response.Authors = await _repository.ListAsync(cancellationToken);
        return response;
    }

    public async ValueTask<GetAuthorByIdResponse> GetAuthorByIdAsync(GetAuthorByIdRequest request, CancellationToken cancellationToken)
    {
        GetAuthorByIdResponse response = new(request.CorrelationId());
        if (request is null) throw new ArgumentNullException(nameof(request));
        _logger.LogInformation($"Create author {(request)}");
        response.AuthorFound = await _repository.GetByIdAsync(request.Id, cancellationToken);
        return response;
    }
}
