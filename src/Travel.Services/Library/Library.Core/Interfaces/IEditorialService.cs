namespace Library.Core.Interfaces;

/// <summary>
/// Editorial service
/// </summary>
public interface IEditorialService
{
    /// <summary>
    /// Create editorial
    /// </summary>
    /// <param name="request">Created model editorial</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Editorial Created</returns>
    ValueTask<CreateEditorialResponse> CreateEditorialAsync(CreateEditorialRequest request, CancellationToken cancellationToken);
    /// <summary>
    /// Delete editorial 
    /// </summary>
    /// <param name="request">Request to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Cancellation token</returns>
    ValueTask<DeleteEditorialResponse> DeleteEditorialAsync(DeleteEditorialRequest request, CancellationToken cancellationToken);
    /// <summary>
    /// Get all editorials 
    /// </summary>
    /// <param name="request">Model request get all editorials</param>
    /// <param name="cancellationToken">Cancellation token request</param>
    /// <returns>List all editorials</returns>
    ValueTask<ListEditorialResponse> GetAllEditorialsAsync(ListEditorialRequest request, CancellationToken cancellationToken);
    /// <summary>
    /// Update editorial 
    /// </summary>
    /// <param name="request">Model to updated</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Model updated</returns>
    ValueTask<UpdateEditorialResponse> UpdateEditorialAsync(UpdateEditorialRequest request, CancellationToken cancellationToken);
}