namespace Identity.Core.Data;

/// <summary>
/// Identity db context
/// </summary>
public class IdentityTravelDbContext: IdentityDbContext<ApplicationUser>
{
    ///// <summary>
    ///// Identity db context constructor
    ///// </summary>
    ///// <param name="options">Options builder</param>
    public IdentityTravelDbContext(DbContextOptions<IdentityTravelDbContext> options): base(options) { }

    /// <summary>
    /// On model creating database, and specific change model
    /// </summary>
    /// <param name="modelBuilder">Model builder application</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(assembly: Assembly.GetExecutingAssembly());
    }
}