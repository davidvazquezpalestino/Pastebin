WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);

// Configurar el cliente HTTP con la URL base de la API
builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri("https://api-pastebin.infosoft.mx/")
    });

// Registrar el servicio de pastes
builder.Services.AddScoped<PasteService>();

// Configurar componentes raíz
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

await builder.Build().RunAsync();
