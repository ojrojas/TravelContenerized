namespace Library.Api.Services
{
    /// <summary>
    /// Book service
    /// </summary>
	public class BookService : BookTravelService.BookTravelServiceBase
	{
        private readonly BookRepository _BookRepository;
        private readonly ILogger<BookService> _logger;
        private readonly IMapper _mapper;

        public BookService(BookRepository BookRepository, ILogger<BookService> logger, IMapper mapper)
        {
            _BookRepository = BookRepository ?? throw new ArgumentNullException(nameof(BookRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Create Book
        /// </summary>
        /// <param name="request">Request Book model</param>
        /// <param name="context">Context Server</param>
        /// <returns>Book created</returns>
        public override async Task<CreateBookResponse> CreateBook(CreateBookRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Create Book request...");
            CreateBookResponse response = new();
            response.BookCreated = _mapper.Map<Book>(
                await _BookRepository.CreateAsync(_mapper.Map<Core.Entities.Book>(request.Book), context.CancellationToken));

            return response;
        }

        /// <summary>
        /// Update Book 
        /// </summary>
        /// <param name="request">Update model</param>
        /// <param name="context">Context call server</param>
        /// <returns></returns>
        public override async Task<UpdateBookResponse> UpdateBook(UpdateBookRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Update Book request...");
            UpdateBookResponse response = new();
            response.BookUpdated = _mapper.Map<Book>(
                await _BookRepository.UpdateAsync(_mapper.Map<Core.Entities.Book>(request.Book), context.CancellationToken));

            return response;
        }

        /// <summary>
        /// Delete Book
        /// </summary>
        /// <param name="request">Id Book to delete</param>
        /// <param name="context">Server context</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public override async Task<DeleteBookResponse> DeleteBook(DeleteBookRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Delete Book by id request...");
            DeleteBookResponse response = new();
            var entityToDelete = await _BookRepository.GetByIdAsync(Guid.Parse(request.Id), context.CancellationToken);
            if (entityToDelete == null) throw new InvalidOperationException("Not found Book to delete");
            response.BookDeleted = _mapper.Map<Book>(
                await _BookRepository.DeleteAsync(entityToDelete, context.CancellationToken));

            return response;
        }

        /// <summary>
        /// Get all Books 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<ListBooksResponse> ListBooks(Empty request, ServerCallContext context)
        {
            _logger.LogInformation("Get all Books request...");
            var list = await _BookRepository.ListAsync(context.CancellationToken);
            ListBooksResponse response = new();
            response.Books.AddRange(list.Select(x => _mapper.Map<Book>(x)));

            return response;
        }

        /// <summary>
        /// Get Book by id
        /// </summary>
        /// <param name="request">Get Book by id</param>
        /// <param name="context">Server call context</param>
        /// <returns>Book found</returns>
        public override async Task<GetBookByIdResponse> GetBookById(GetBookByIdRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Get Book by id request...");
            GetBookByIdResponse response = new();
            response.Book = _mapper.Map<Book>(await _BookRepository.GetByIdAsync(Guid.Parse(request.Id), context.CancellationToken));

            return response;
        }
    }
}

