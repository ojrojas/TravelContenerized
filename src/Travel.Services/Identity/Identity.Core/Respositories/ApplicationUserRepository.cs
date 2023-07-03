namespace Identity.Core.Respositories;

public class ApplicationUserRepository : GenericRepository<ApplicationUser>
{
    public ApplicationUserRepository(ILogger<GenericRepository<ApplicationUser>> logger,
                                     IdentityTravelDbContext context) : base(logger, context)
    {
    }
}

