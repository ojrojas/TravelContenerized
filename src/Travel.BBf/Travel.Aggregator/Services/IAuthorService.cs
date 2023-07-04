namespace Travel.Aggregator.Services;

/// <summary>
/// Author interface service
/// </summary>
public interface IAuthorService
{
    /// <summary>
    /// Create author
    /// </summary>
    /// <param name="request">Request grpc</param>
    /// <param name="options">Call context</param>
    /// <returns>Author created</returns>
    ValueTask<CreateAuthorResponse> CreateAuthorAsync(CreateAuthorRequest request, CallOptions options);

    /// <summary>
    /// Delete author
    /// </summary>
    /// <param name="request">Request grpc</param>
    /// <param name="options">Call context</param>
    /// <returns>Author deleted</returns>
    ValueTask<DeleteAuthorResponse> DeleteAuthorAsync(DeleteAuthorRequest request, CallOptions options);

    /// <summary>
    /// Get all authors 
    /// </summary>
    /// <param name="options">Call context</param>
    /// <returns>List authors</returns>
    ValueTask<ListAuthorsResponse> GetAllAuthorsAsync(CallOptions options);

    /// <summary>
    /// Get author by id 
    /// </summary>
    /// <param name="request">Request grpc</param>
    /// <param name="options">Call context</param>
    /// <returns>Author founded</returns>
    ValueTask<GetAuthorByIdResponse> GetAuthorByIdAsync(GetAuthorByIdRequest request, CallOptions options);

    /// <summary>
    /// Update author 
    /// </summary>
    /// <param name="request">Request grpc</param>
    /// <param name="options">Call context</param>
    /// <returns>Author updated</returns>
    ValueTask<UpdateAuthorResponse> UpdateAuthorAsync(UpdateAuthorRequest request, CallOptions options);
}