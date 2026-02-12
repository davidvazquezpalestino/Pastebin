namespace Pastebin.Infrastructure;

public class PastebinDbContext : DbContext
{
    public PastebinDbContext(DbContextOptions<PastebinDbContext> options) : base(options)
    {
    }
    public PastebinDbContext()
    {
    }

    public DbSet<Paste> Pastes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Paste>(entity =>
        {
            entity.HasKey(e => e.PasteID);
            entity.Property(e => e.PasteID).HasMaxLength(32);
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.Content).IsRequired();
            entity.Property(e => e.Language).HasMaxLength(50);
            entity.Property(e => e.CreatedAt).IsRequired();
        });
    }
}