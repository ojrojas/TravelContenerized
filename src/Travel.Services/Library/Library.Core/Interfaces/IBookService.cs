namespace Library.Core.Interfaces;

/// <summary>
/// Book service
/// </summary>
public interface IBookService
{
    /// <summary>
    /// Create book
    /// </summary>
    /// <param name="request">Mode book create</param>
    /// <param name="cancellationToken">Cancellation token request</param>
    /// <returns>Model book created</returns>
    ValueTask<CreateBookResponse> CreateBookAsync(CreateBookRequest request, CancellationToken cancellationToken);
    /// <summary>
    /// Delete book
    /// </summary>
    /// <param name="request">Model request to delete</param>
    /// <param name="cancellationToken">Cancellation token request</param>
    /// <returns>Model book deleted</returns>
    ValueTask<DeleteBookResponse> DeleteBookAsync(DeleteBookRequest request, CancellationToken cancellationToken);
    /// <summary>
    /// Get all books 
    /// </summary>
    /// <param name="request">Model request all books</param>
    /// <param name="cancellationToken">Cancellation token request</param>
    /// <returns>List books response</returns>
    ValueTask<ListBooksResponse> GetAllBooksAsync(ListBooksRequest request, CancellationToken cancellationToken);
    /// <summary>
    /// Update book 
    /// </summary>
    /// <param name="request">Model updated request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Updated model book</returns>
    ValueTask<UpdateBookResponse> UpdateBookAsync(UpdateBookRequest request, CancellationToken cancellationToken);
}