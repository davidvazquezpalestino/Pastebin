using System;

namespace Pastebin.Api.Models
{
    public class CreatePasteRequest
    {
        public string Title { get; set; }
        public string Language { get; set; }
        public string Content { get; set; }
        public int? ExpireInMinutes { get; set; }
    }
}
