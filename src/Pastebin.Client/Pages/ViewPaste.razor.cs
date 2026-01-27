namespace Pastebin.Client.Pages;

public partial class ViewPaste
{
    [Parameter] public string Id { get; set; }

    private PasteResponse Paste { get; set; }
    private bool IsLoading { get; set; } = true;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            Paste = await PasteService.GetPasteAsync(Id);
            await JsRuntime.InvokeVoidAsync("Prism.highlightAll");
        }
        catch (Exception ex)
        {
            await Console.Error.WriteLineAsync($"Error al cargar el paste: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    private async Task CopyToClipboard()
    {
        await JsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", Paste.Content);
    }

    private string GetLanguageClass(string language)
    {
        return language switch
        {
            "csharp" => "language-csharp",
            "javascript" => "language-js",
            "python" => "language-python",
            "java" => "language-java",
            "cpp" => "language-cpp",
            "html" => "language-html",
            "css" => "language-css",
            "sql" => "language-sql",
            "text" => "",
            _ => ""
        };
    }
}