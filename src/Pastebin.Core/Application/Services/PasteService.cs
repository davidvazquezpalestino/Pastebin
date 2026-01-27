namespace Pastebin.Core.Application.Services
{
    public class PasteService(IPasteRepository pasteRepository)
    {
        private readonly IPasteRepository PasteRepository = pasteRepository ?? throw new ArgumentNullException(nameof(pasteRepository));

        public async Task<Paste> CreatePasteAsync(string content, TimeSpan? expiration = null, string title = null, string language = null)
        {
            Paste paste = Paste.Create(content, expiration, title, language);
            await PasteRepository.AddAsync(paste);
            return paste;
        }

        public async Task<Paste> GetPasteAsync(string id)
        {
            return await PasteRepository.GetByIdAsync(id);
        }

        public async Task DeletePasteAsync(string id)
        {
            await PasteRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Paste>> GetAllPastesAsync()
        {
            return await PasteRepository.GetAllAsync();
        }
    }
}
