namespace Pastebin.Client.Pages;

public partial class Index
{
    private CreatePasteRequest CreateRequest { get; set; } = new();
    private bool IsSubmitting { get; set; }
    private async Task HandleValidSubmit()
    {
        IsSubmitting = true;
        try
        {
            PasteResponse response = await PasteService.CreatePasteAsync(CreateRequest);
            NavigationManager.NavigateTo($"/paste/{response.Id}");
        }
        catch (Exception ex)
        {
            await Console.Error.WriteLineAsync($"Error al crear el paste: {ex.Message}");
        }
        finally
        {
            IsSubmitting = false;
        }
    }
}