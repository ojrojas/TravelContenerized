namespace Travel.Aggregator.Services;

public interface IEditorialService
{
    /// <summary>
    /// Create Editorial
    /// </summary>
    /// <param name="request">Request grpc</param>
    /// <param name="options">Call context</param>
    /// <returns>Editorial created</returns>
    ValueTask<CreateEditorialResponse> CreateEditorialAsync(CreateEditorialRequest request, CallOptions options);

    /// <summary>
    /// Delete Editorial
    /// </summary>
    /// <param name="request">Request grpc</param>
    /// <param name="options">Call context</param>
    /// <returns>Editorial deleted</returns>
    ValueTask<DeleteEditorialResponse> DeleteEditorialAsync(DeleteEditorialRequest request, CallOptions options);

    /// <summary>
    /// Get all Editorials 
    /// </summary>
    /// <param name="options">Call context</param>
    /// <returns>List Editorials</returns>
    ValueTask<ListEditorialsResponse> GetAllEditorialsAsync(CallOptions options);

    /// <summary>
    /// Get Editorial by id 
    /// </summary>
    /// <param name="request">Request grpc</param>
    /// <param name="options">Call context</param>
    /// <returns>Editorial founded</returns>
    ValueTask<GetEditorialByIdResponse> GetEditorialByIdAsync(GetEditorialByIdRequest request, CallOptions options);

    /// <summary>
    /// Update Editorial 
    /// </summary>
    /// <param name="request">Request grpc</param>
    /// <param name="options">Call context</param>
    /// <returns>Editorial updated</returns>
    ValueTask<UpdateEditorialResponse> UpdateEditorialAsync(UpdateEditorialRequest request, CallOptions options);
}