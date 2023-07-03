namespace Library.Core.Interfaces;

/// <summary>
/// Author services interface
/// </summary>
public interface IAuthorService
{
    /// <summary>
    /// Create author async 
    /// </summary>
    /// <param name="request">Model to created author</param>
    /// <param name="cancellationToken">Cancellation token request</param>
    /// <returns>Author Created</returns>
    ValueTask<CreateAuthorResponse> CreateAuthorAsync(CreateAuthorRequest request, CancellationToken cancellationToken);
    /// <summary>
    /// Delete author async
    /// </summary>
    /// <param name="request">Model to delete author</param>
    /// <param name="cancellationToken">Cancellation token request</param>
    /// <returns>Author Deleted</returns>
    ValueTask<DeleteAuthorResponse> DeleteAuthorAsync(DeleteAuthorRequest request, CancellationToken cancellationToken);
    /// <summary>
    /// Get all author async
    /// </summary>
    /// <param name="request">Model request get all authors</param>
    /// <param name="cancellationToken">Cancellation token request</param>
    /// <returns>List author in application</returns>
    ValueTask<ListAuthorResponse> GetAllAuthorsAsync(ListAuthorRequest request, CancellationToken cancellationToken);
    /// <summary>
    /// Get author by id async
    /// </summary>
    /// <param name="request">Model request author</param>
    /// <param name="cancellationToken">Cancellation token request</param>
    /// <returns>Author requested</returns>
    ValueTask<GetAuthorByIdResponse> GetAuthorByIdAsync(GetAuthorByIdRequest request, CancellationToken cancellationToken);
    /// <summary>
    /// Update author
    /// </summary>
    /// <param name="request">Model to update</param>
    /// <param name="cancellationToken">Cancellation token request</param>
    /// <returns>Author updated</returns>
    ValueTask<UpdateAuthorResponse> UpdateAuthorAsync(UpdateAuthorRequest request, CancellationToken cancellationToken);
}