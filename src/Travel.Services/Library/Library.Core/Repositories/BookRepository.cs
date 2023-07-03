namespace Library.Core.Repositories;

/// <summary>
/// Book repository
/// </summary>
public class BookRepository : GenericRepository<Book>
{
    public BookRepository(ILogger<GenericRepository<Book>> logger, LibraryDbContext context) : base(logger, context)
    {
    }
}

