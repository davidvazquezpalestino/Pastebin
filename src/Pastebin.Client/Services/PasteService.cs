using System.Net.Http.Json;

namespace Pastebin.Client.Services
{
    public class PasteService(HttpClient httpClient)
    {
        private const string BaseApiPath = "api/pastes";

        public async Task<PasteResponse> CreatePasteAsync(CreatePasteRequest request)
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync(BaseApiPath, request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<PasteResponse>();
        }

        public async Task<PasteResponse> GetPasteAsync(string id)
        {
            return await httpClient.GetFromJsonAsync<PasteResponse>($"{BaseApiPath}/{id}");
        }

        public async Task<List<PasteResponse>> GetAllPastesAsync()
        {
            return await httpClient.GetFromJsonAsync<List<PasteResponse>>(BaseApiPath);
        }
    }
}
