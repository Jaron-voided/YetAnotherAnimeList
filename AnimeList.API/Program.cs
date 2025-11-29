using System.Text.Json.Serialization;
using AnimeList.Application.Handlers.Anime.Query;
using AnimeList.Application.Interfaces;
using AnimeList.Application.Mapping;
using AnimeList.Persistence.CSV;
using AnimeList.Persistence.Database;
using AnimeList.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Connection string
var connectionString = builder.Configuration.GetConnectionString("AnimeListDatabase")
                       ?? throw new InvalidOperationException("Connection string `AnimeListDatabase` not found");

var csvLocation = builder.Configuration["CsvSettings:DetailsPath"]
                  ?? throw new InvalidOperationException("CsvSettings:DetailsPath not found");

// register API services
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition =
            JsonIgnoreCondition.WhenWritingNull;
    });

// register persistence layer
builder.Services.AddSingleton<IDbConnectionFactory>(_ => 
    new SqliteConnectionFactory(connectionString));

builder.Services.AddScoped<IAnimeLoadRepository, AnimeLoadRepository>();

builder.Services.AddScoped<IAnimeRepository, AnimeRepository>();
builder.Services.AddScoped<AnimeQueryHandler>();

builder.Services.AddSingleton<IAnimeMapper, AnimeMapper>();

// CSV Parse and mapping
builder.Services.AddSingleton<CsvAnimeParser>(_ =>
    new CsvAnimeParser(csvLocation));

builder.Services.AddSingleton<RawAnimeMapper>();

builder.Services.AddSingleton<DbInitializer>();
builder.Services.AddScoped<AnimeDbOrchestrator>();

var app = builder.Build();

// run DB init once at startup and load the data if not previously done
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    
    var initializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();
    await initializer.InitializeDbAsync();
    
    var repo = services.GetRequiredService<IAnimeLoadRepository>();
    var hasData = await repo.HasBeenLoadedAsync();

    if (!hasData)
    {
        var orchestrator = services.GetRequiredService<AnimeDbOrchestrator>();
        await orchestrator.SeedDatabaseAsync();
    }
}

app.MapControllers();

app.Run();

public partial class Program { }
