namespace Library.Core.Services;

public class BookService : IBookService
{
    private readonly BookRepository _repository;
    private readonly ILogger<BookService> _logger;

    public BookService(BookRepository repository, ILogger<BookService> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async ValueTask<CreateBookResponse> CreateBookAsync(CreateBookRequest request, CancellationToken cancellationToken)
    {
        CreateBookResponse response = new(request.CorrelationId());
        if (request.Book is null) throw new ArgumentNullException(nameof(request));
        _logger.LogInformation($"Create author {JsonSerializer.Serialize(request.Book)}");
        response.BookCreated = await _repository.CreateAsync(request.Book, cancellationToken);
        return response;
    }

    public async ValueTask<DeleteBookResponse> DeleteBookAsync(DeleteBookRequest request, CancellationToken cancellationToken)
    {
        DeleteBookResponse response = new(request.CorrelationId());
        if (request.Id.Equals(Guid.Empty)) throw new ArgumentNullException(nameof(request));
        _logger.LogInformation($"Create author {JsonSerializer.Serialize(request.Id)}");
        var author = await _repository.GetByIdAsync(request.Id, cancellationToken);
        response.BookDeleted = await _repository.DeleteAsync(author, cancellationToken);
        return response;
    }

    public async ValueTask<UpdateBookResponse> UpdateBookAsync(UpdateBookRequest request, CancellationToken cancellationToken)
    {
        UpdateBookResponse response = new(request.CorrelationId());
        if (request.Book is null) throw new ArgumentNullException(nameof(request));
        _logger.LogInformation($"Create author {JsonSerializer.Serialize(request.Book)}");
        response.BookUpdated = await _repository.UpdateAsync(request.Book, cancellationToken);
        return response;
    }

    public async ValueTask<ListBooksResponse> GetAllBooksAsync(ListBooksRequest request, CancellationToken cancellationToken)
    {
        ListBooksResponse response = new(request.CorrelationId());
        if (request is null) throw new ArgumentNullException(nameof(request));
        _logger.LogInformation($"Create author {JsonSerializer.Serialize(request)}");
        response.Books = await _repository.ListAsync(cancellationToken);
        return response;
    }
}
