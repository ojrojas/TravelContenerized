namespace Library.Core.Contexts;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
    {
    }

    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Editorial> Editorials { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(assembly: Assembly.GetExecutingAssembly());
        modelBuilder.Entity<AuthorsBooks>()
       .HasKey(bc => new { bc.BookId, bc.AuthorId });
        modelBuilder.Entity<AuthorsBooks>()
            .HasOne(bc => bc.Book)
            .WithMany(b => b.AuthorsBooks)
            .HasForeignKey(bc => bc.BookId);
        modelBuilder.Entity<AuthorsBooks>()
            .HasOne(bc => bc.Author)
            .WithMany(c => c.AuthorsBooks)
            .HasForeignKey(bc => bc.AuthorId);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}