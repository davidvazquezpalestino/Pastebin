namespace Pastebin.Core.Domain.Repositories
{
    public interface IPasteRepository
    {
        Task<Paste> GetByIdAsync(string id);
        Task<IEnumerable<Paste>> GetAllAsync();
        Task AddAsync(Paste paste);
        Task UpdateAsync(Paste paste);
        Task DeleteAsync(string id);
    }
}
