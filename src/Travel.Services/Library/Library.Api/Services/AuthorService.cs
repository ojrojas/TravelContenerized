namespace Library.Api.Services;

/// <summary>
/// Author Service
/// </summary>
public class AuthorService : AuthorTravelService.AuthorTravelServiceBase
{
    private readonly AuthorRepository _authorRepository;
    private readonly ILogger<AuthorService> _logger;
    private readonly IMapper _mapper;

    public AuthorService(AuthorRepository authorRepository, ILogger<AuthorService> logger, IMapper mapper)
    {
        _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <summary>
    /// Create author
    /// </summary>
    /// <param name="request">Request author model</param>
    /// <param name="context">Context Server</param>
    /// <returns>Author created</returns>
    public override async Task<CreateAuthorResponse> CreateAuthor(CreateAuthorRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Create author request...");
        CreateAuthorResponse response = new();
        response.AuthorCreated = _mapper.Map<Author>(
            await _authorRepository.CreateAsync(_mapper.Map<Core.Entities.Author>(request.Author), context.CancellationToken));

        return response;
    }

    /// <summary>
    /// Update author 
    /// </summary>
    /// <param name="request">Update model</param>
    /// <param name="context">Context call server</param>
    /// <returns></returns>
    public override async Task<UpdateAuthorResponse> UpdateAuthor(UpdateAuthorRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Update author request...");
        UpdateAuthorResponse response = new();
        response.AuthorUpdated = _mapper.Map<Author>(
            await _authorRepository.UpdateAsync(_mapper.Map<Core.Entities.Author>(request.Author), context.CancellationToken));

        return response;
    }

    /// <summary>
    /// Delete author
    /// </summary>
    /// <param name="request">Id author to delete</param>
    /// <param name="context">Server context</param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public override async Task<DeleteAuthorResponse> DeleteAuthor(DeleteAuthorRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Delete author by id request...");
        DeleteAuthorResponse response = new();
        var entityToDelete = await _authorRepository.GetByIdAsync(Guid.Parse(request.Id), context.CancellationToken);
        if (entityToDelete == null) throw new InvalidOperationException("Not found author to delete");
        response.AuthorDeleted = _mapper.Map<Author>(
            await _authorRepository.DeleteAsync(entityToDelete, context.CancellationToken));

        return response;
    }

    /// <summary>
    /// Get all authors 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<ListAuthorsResponse> ListAuthors(Empty request, ServerCallContext context)
    {
        _logger.LogInformation("Get all authors request...");
        var list = await _authorRepository.ListAsync(context.CancellationToken);
        ListAuthorsResponse response = new();
        response.Authors.AddRange(list.Select(x=> _mapper.Map<Author>(x)));

        return response;
    }

    /// <summary>
    /// Get author by id
    /// </summary>
    /// <param name="request">Get author by id</param>
    /// <param name="context">Server call context</param>
    /// <returns>Author found</returns>
    public override async Task<GetAuthorByIdResponse> GetAuthorById(GetAuthorByIdRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Get author by id request...");
        GetAuthorByIdResponse response = new();
        response.Author = _mapper.Map<Author>(await _authorRepository.GetByIdAsync(Guid.Parse(request.Id), context.CancellationToken));

        return response;
    }
}

