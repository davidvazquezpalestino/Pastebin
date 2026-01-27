namespace Pastebin.Infrastructure.Persistence.Repositories
{
    public class SqlPasteRepository(PastebinDbContext dbContext) : IPasteRepository
    {
        private readonly PastebinDbContext DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        public async Task<Paste> GetByIdAsync(string id)
        {
            return await DbContext.Pastes.FindAsync(id);
        }

        public async Task<IEnumerable<Paste>> GetAllAsync()
        {
            return await DbContext.Pastes.ToListAsync();
        }

        public async Task AddAsync(Paste paste)
        {
            if (paste == null)
            {
                throw new ArgumentNullException(nameof(paste));
            }

            DbContext.Pastes.Add(paste);
            await DbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Paste paste)
        {
            if (paste == null)
            {
                throw new ArgumentNullException(nameof(paste));
            }

            DbContext.Pastes.Update(paste);
            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            Paste paste = await DbContext.Pastes.FindAsync(id);
            if (paste != null)
            {
                DbContext.Pastes.Remove(paste);
                await DbContext.SaveChangesAsync();
            }
        }
    }
}
