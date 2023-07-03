namespace Library.Core.Repositories;

/// <summary>
/// Editorial repository
/// </summary>
public class EditorialRepository : GenericRepository<Editorial>
{
    public EditorialRepository(ILogger<GenericRepository<Editorial>> logger, LibraryDbContext context) : base(logger, context)
    {
    }
}

