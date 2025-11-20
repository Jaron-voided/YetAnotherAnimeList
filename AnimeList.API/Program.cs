using AnimeProject.Application.Interfaces;
using AnimeProject.Persistence.CSV;
using AnimeProject.Persistence.Database;
using AnimeProject.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Connection string
var connectionString = builder.Configuration.GetConnectionString("AnimeListDatabase")
                       ?? throw new InvalidOperationException("Connection string `AnimeListDatabase` not found");

var csvLocation = builder.Configuration["CsvSettings:DetailsPath"]
                  ?? throw new InvalidOperationException("CsvSettings:DetailsPath not found");

// register API services
builder.Services.AddControllers();

// register persistence layer
builder.Services.AddSingleton<IDbConnectionFactory>(_ => 
    new SqliteConnectionFactory(connectionString));

builder.Services.AddScoped<IAnimeRepository, AnimeRepository>();

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
    
    var repo = services.GetRequiredService<IAnimeRepository>();
    var hasData = await repo.HasBeenLoadedAsync();

    if (!hasData)
    {
        var orchestrator = services.GetRequiredService<AnimeDbOrchestrator>();
        await orchestrator.SeedDatabaseAsync();
    }
}

app.MapControllers();

app.Run();
