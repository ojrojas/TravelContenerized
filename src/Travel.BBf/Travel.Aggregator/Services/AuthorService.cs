namespace Travel.Aggregator.Services;
/// <summary>
/// Author service
/// </summary>
public class AuthorService : IAuthorService
{
    /// <summary>
    /// Logger application
    /// </summary>
	private readonly ILogger<AuthorService> _logger;
    /// <summary>
    /// Client gprc
    /// </summary>
    private readonly AuthorTravelService.AuthorTravelServiceClient _client;

    public AuthorService(ILogger<AuthorService> logger, AuthorTravelService.AuthorTravelServiceClient client)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async ValueTask<CreateAuthorResponse> CreateAuthorAsync(CreateAuthorRequest request, CallOptions options)
    {
        _logger.LogInformation("Request client grpc create comunication");
        return await _client.CreateAuthorAsync(request, options);
    }

    public async ValueTask<UpdateAuthorResponse> UpdateAuthorAsync(UpdateAuthorRequest request, CallOptions options)
    {
        _logger.LogInformation("Request client grpc update comunication");
        return await _client.UpdateAuthorAsync(request, options);
    }

    public async ValueTask<DeleteAuthorResponse> DeleteAuthorAsync(DeleteAuthorRequest request, CallOptions options)
    {
        _logger.LogInformation("Request client grpc delete comunication");
        return await _client.DeleteAuthorAsync(request, options);
    }

    public async ValueTask<ListAuthorsResponse> GetAllAuthorsAsync(CallOptions options)
    {
        _logger.LogInformation("Request client grpc get all authors comunication");
        return await _client.ListAuthorsAsync(new Google.Protobuf.WellKnownTypes.Empty(), options);
    }

    public async ValueTask<GetAuthorByIdResponse> GetAuthorByIdAsync(GetAuthorByIdRequest request, CallOptions options)
    {
        _logger.LogInformation("Request client grpc get by id author comunication");
        return await _client.GetAuthorByIdAsync(request, options);
    }
}

