namespace Library.Core.Repositories;

/// <summary>
/// Author repository
/// </summary>
public class AuthorRepository : GenericRepository<Author>
{
    public AuthorRepository(ILogger<GenericRepository<Author>> logger, LibraryDbContext context) : base(logger, context)
    {
    }
}

