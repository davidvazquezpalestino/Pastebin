using Microsoft.EntityFrameworkCore.Design;

namespace Pastebin.Infrastructure;

public class PastebinDbContextFactory : IDesignTimeDbContextFactory<PastebinDbContext>
{
    public PastebinDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PastebinDbContext>();
        optionsBuilder.UseSqlServer("Server=74.208.218.86;Database=Pastebin;User Id=sa;Password=MSsql2026;MultipleActiveResultSets=true;encrypt=false;");

        return new PastebinDbContext(optionsBuilder.Options);
    }
}