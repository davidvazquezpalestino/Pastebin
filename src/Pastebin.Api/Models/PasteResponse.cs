using Pastebin.Core.Domain;

namespace Pastebin.Api.Models
{
    public class PasteResponse
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string Url { get; set; }

        public static PasteResponse FromDomain(Paste paste, string baseUrl)
        {
            if (paste == null)
            {
                return null;
            }

            return new PasteResponse
            {
                Id = paste.Id,
                Title = paste.Title,
                Language = paste.Language,
                Content = paste.Content,
                CreatedAt = paste.CreatedAt,
                ExpirationDate = paste.ExpirationDate,
                Url = $"{baseUrl}/api/pastes/{paste.Id}"
            };
        }
    }
}
