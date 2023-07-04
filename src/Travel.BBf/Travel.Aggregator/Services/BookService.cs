namespace Travel.Aggregator.Services;

public class BookService : IBookService
{
    /// <summary>
    /// Logger application
    /// </summary>
    private readonly ILogger<BookService> _logger;
    /// <summary>
    /// Client gprc
    /// </summary>
    private readonly BookTravelService.BookTravelServiceClient _client;

    public BookService(ILogger<BookService> logger, BookTravelService.BookTravelServiceClient client)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async ValueTask<CreateBookResponse> CreateBookAsync(CreateBookRequest request, CallOptions options)
    {
        _logger.LogInformation("Request client grpc create comunication");
        return await _client.CreateBookAsync(request, options);
    }

    public async ValueTask<UpdateBookResponse> UpdateBookAsync(UpdateBookRequest request, CallOptions options)
    {
        _logger.LogInformation("Request client grpc update comunication");
        return await _client.UpdateBookAsync(request, options);
    }

    public async ValueTask<DeleteBookResponse> DeleteBookAsync(DeleteBookRequest request, CallOptions options)
    {
        _logger.LogInformation("Request client grpc delete comunication");
        return await _client.DeleteBookAsync(request, options);
    }

    public async ValueTask<ListBooksResponse> GetAllBooksAsync(CallOptions options)
    {
        _logger.LogInformation("Request client grpc get all Books comunication");
        return await _client.ListBooksAsync(new Google.Protobuf.WellKnownTypes.Empty(), options);
    }

    public async ValueTask<GetBookByIdResponse> GetBookByIdAsync(GetBookByIdRequest request, CallOptions options)
    {
        _logger.LogInformation("Request client grpc get by id Book comunication");
        return await _client.GetBookByIdAsync(request, options);
    }
}

