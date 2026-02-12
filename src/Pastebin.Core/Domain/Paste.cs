namespace Pastebin.Core.Domain
{
    public class Paste
    {
        public string PasteID { get; private set; }
        public string Title { get; private set; }
        public string Language { get; private set; }
        public string Content { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? ExpirationDate { get; private set; }
        public bool IsExpired => ExpirationDate.HasValue && DateTime.UtcNow > ExpirationDate.Value;

        private Paste() { }

        public static Paste Create(string content, TimeSpan? expiration = null, string title = null, string language = null)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentException("Content cannot be empty", nameof(content));
            }

            return new Paste
            {
                PasteID = Guid.NewGuid().ToString("N"),
                Title = string.IsNullOrWhiteSpace(title) ? "Untitled" : title,
                Language = string.IsNullOrWhiteSpace(language) ? "text" : language,
                Content = content,
                CreatedAt = DateTime.UtcNow,
                ExpirationDate = expiration.HasValue ? DateTime.UtcNow.Add(expiration.Value) : (DateTime?)null
            };
        }
    }
}
