namespace Pastebin.Client.Models
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
    }
}
