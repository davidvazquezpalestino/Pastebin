namespace Pastebin.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PastesController(PasteService pasteService, IConfiguration configuration) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> CreatePaste([FromBody] CreatePasteRequest request)
        {
            if (string.IsNullOrWhiteSpace(request?.Content))
            {
                return BadRequest("Content is required");
            }

            TimeSpan? expiration = request.ExpireInMinutes.HasValue
                ? TimeSpan.FromMinutes(request.ExpireInMinutes.Value)
                : (TimeSpan?)null;

            Paste paste = await pasteService.CreatePasteAsync(request.Content, expiration, request.Title, request.Language);
            string baseUrl = configuration["ClientUrl"];

            return CreatedAtAction(
                nameof(GetPaste),
                new { id = paste.PasteID },
                PasteResponse.FromDomain(paste, baseUrl));
        }

        [HttpGet]
        public async Task<IActionResult> GetPastes()
        {
            IEnumerable<Paste> pastes = await pasteService.GetAllPastesAsync();
            string baseUrl = configuration["ClientUrl"];
            IEnumerable<PasteResponse> responses = pastes.Select(p => PasteResponse.FromDomain(p, baseUrl));
            return Ok(responses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaste(string id)
        {
            Paste paste = await pasteService.GetPasteAsync(id);

            if (paste == null)
            {
                return NotFound("Paste not found or has expired");
            }

            string baseUrl = configuration["ClientUrl"];
            return Ok(PasteResponse.FromDomain(paste, baseUrl));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaste(string id)
        {
            // TODO: Add admin authentication
            await pasteService.DeletePasteAsync(id);
            return NoContent();
        }
    }
}
