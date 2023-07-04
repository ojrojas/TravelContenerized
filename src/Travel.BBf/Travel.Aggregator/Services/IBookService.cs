namespace Travel.Aggregator.Services;

public interface IBookService
{
    /// <summary>
    /// Create Book
    /// </summary>
    /// <param name="request">Request grpc</param>
    /// <param name="options">Call context</param>
    /// <returns>Book created</returns>
    ValueTask<CreateBookResponse> CreateBookAsync(CreateBookRequest request, CallOptions options);

    /// <summary>
    /// Delete Book
    /// </summary>
    /// <param name="request">Request grpc</param>
    /// <param name="options">Call context</param>
    /// <returns>Book deleted</returns>
    ValueTask<DeleteBookResponse> DeleteBookAsync(DeleteBookRequest request, CallOptions options);

    /// <summary>
    /// Get all Books 
    /// </summary>
    /// <param name="options">Call context</param>
    /// <returns>List Books</returns>
    ValueTask<ListBooksResponse> GetAllBooksAsync(CallOptions options);

    /// <summary>
    /// Get Book by id 
    /// </summary>
    /// <param name="request">Request grpc</param>
    /// <param name="options">Call context</param>
    /// <returns>Book founded</returns>
    ValueTask<GetBookByIdResponse> GetBookByIdAsync(GetBookByIdRequest request, CallOptions options);

    /// <summary>
    /// Update Book 
    /// </summary>
    /// <param name="request">Request grpc</param>
    /// <param name="options">Call context</param>
    /// <returns>Book updated</returns>
    ValueTask<UpdateBookResponse> UpdateBookAsync(UpdateBookRequest request, CallOptions options);
}