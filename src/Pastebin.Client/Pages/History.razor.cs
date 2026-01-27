namespace Pastebin.Client.Pages;

public partial class History
{
    private List<PasteResponse> Pastes { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            Pastes = await PasteService.GetAllPastesAsync();
        }
        catch (Exception)
        {
            Pastes = new List<PasteResponse>();
        }
    }
}