using Microsoft.EntityFrameworkCore;
using Pastebin.Api;
using Pastebin.Core.Domain.Repositories;
using Pastebin.Infrastructure;
using Pastebin.Infrastructure.Persistence.Repositories;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

// Add services to the container.
builder.Services.AddControllers();

// Add CORS policy to allow requests from the Blazor WebAssembly client
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Register application services
builder.Services.AddScoped<PasteService>();

// Add DbContext
builder.Services.AddDbContext<PastebinDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories
builder.Services.AddScoped<IPasteRepository, SqlPasteRepository>();

// Register the configuration
builder.Services.Configure<PastebinSettings>(builder.Configuration.GetSection("Pastebin"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.MapControllers();


app.Run();