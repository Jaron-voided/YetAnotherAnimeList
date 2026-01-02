using AnimeList.Application.Interfaces.Anime;
using AnimeList.Application.RepoInterfaces.Anime;
using AnimeList.Application.RepoInterfaces.AnimeRatings;
using AnimeList.Application.RepoInterfaces.AnimeRecommendations;
using AnimeList.Application.RepoInterfaces.AnimeStats;
using AnimeList.Persistence.Database;
using AnimeList.Persistence.Database.Orchestrator;

namespace AnimeList.API.HostedServices;

public class DatabaseStartupService : BackgroundService
{
    private readonly IServiceProvider _services;

    public DatabaseStartupService(IServiceProvider services)
    {
        _services = services;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _services.CreateScope();
        var sp = scope.ServiceProvider;

        var initializer = sp.GetRequiredService<DbInitializer>();
        await initializer.InitializeDbAsync();

        var animeRepo = sp.GetRequiredService<IAnimeLoadRepository>();
        var statsRepo = sp.GetRequiredService<IAnimeStatsLoadRepository>();
        var recommendationsRepo = sp.GetRequiredService<IAnimeRecommendationsLoadRepository>();
        var ratingsRepo = sp.GetRequiredService<IAnimeRatingsLoadRepository>();

        var hasAnime = await animeRepo.HasBeenLoadedAsync();
        var hasStats = await statsRepo.HasBeenLoadedAsync();
        var hasRecommendations = await recommendationsRepo.HasBeenLoadedAsync();
        var hasRatings = await ratingsRepo.HasBeenLoadedAsync();

        if (!hasAnime || !hasStats || !hasRecommendations || !hasRatings)
        {
            var orchestrator = sp.GetRequiredService<AnimeDbOrchestrator>();
            await orchestrator.SeedDatabaseAsync();
        }
        
    }

}