namespace Travel.Aggregator.Services;

public class EditorialService : IEditorialService
{
    /// <summary>
    /// Logger application
    /// </summary>
    private readonly ILogger<EditorialService> _logger;
    /// <summary>
    /// Client gprc
    /// </summary>
    private readonly EditorialTravelService.EditorialTravelServiceClient _client;

    public EditorialService(ILogger<EditorialService> logger, EditorialTravelService.EditorialTravelServiceClient client)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async ValueTask<CreateEditorialResponse> CreateEditorialAsync(CreateEditorialRequest request, CallOptions options)
    {
        _logger.LogInformation("Request client grpc create comunication");
        return await _client.CreateEditorialAsync(request, options);
    }

    public async ValueTask<UpdateEditorialResponse> UpdateEditorialAsync(UpdateEditorialRequest request, CallOptions options)
    {
        _logger.LogInformation("Request client grpc update comunication");
        return await _client.UpdateEditorialAsync(request, options);
    }

    public async ValueTask<DeleteEditorialResponse> DeleteEditorialAsync(DeleteEditorialRequest request, CallOptions options)
    {
        _logger.LogInformation("Request client grpc delete comunication");
        return await _client.DeleteEditorialAsync(request, options);
    }

    public async ValueTask<ListEditorialsResponse> GetAllEditorialsAsync(CallOptions options)
    {
        _logger.LogInformation("Request client grpc get all Editorials comunication");
        return await _client.ListEditorialsAsync(new Google.Protobuf.WellKnownTypes.Empty(), options);
    }

    public async ValueTask<GetEditorialByIdResponse> GetEditorialByIdAsync(GetEditorialByIdRequest request, CallOptions options)
    {
        _logger.LogInformation("Request client grpc get by id Editorial comunication");
        return await _client.GetEditorialByIdAsync(request, options);
    }
}

