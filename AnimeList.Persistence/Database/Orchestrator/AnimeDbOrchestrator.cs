using AnimeList.Persistence.Diagnostics;

namespace AnimeList.Persistence.Database.Orchestrator;

public class AnimeDbOrchestrator
{
    private readonly SeedAnime _seedAnime;
    private readonly SeedAnimeStats _seedAnimeStats;
    private readonly SeedAnimeRecommendations _seedAnimeRecommendations;
    private readonly SeedAnimeRatings _seedAnimeRatings;

    public AnimeDbOrchestrator(
        SeedAnime seedAnime,
        SeedAnimeStats seedAnimeStats,
        SeedAnimeRecommendations seedAnimeRecommendations,
        SeedAnimeRatings seedAnimeRatings
    )
    {
        _seedAnime = seedAnime;
        _seedAnimeStats = seedAnimeStats;
        _seedAnimeRecommendations = seedAnimeRecommendations;
        _seedAnimeRatings = seedAnimeRatings;
    }

    /*public async Task SeedDatabaseAsync()
    {
        
        IEnumerable<RawAnimeDto> rawAnimeDtos = _csvAnimeParser.Parse();
        
        var cleanAnimes = MapAllAnimes(rawAnimeDtos);

        await _animeLoadRepository.InsertAllAnimeAsync(cleanAnimes);
    }*/

    public async Task SeedDatabaseAsync()
    {
        Profiler.SetDefaultBufferSize(1024);
        Profiler.Begin("Database Seed from startup service: AnimeDbOrchestrator");
        
        Profiler.Begin("AnimeDatabase seed from orchestrator");
        Console.WriteLine("Seeding Anime database...");
        await _seedAnime.SeedAnimeAsync();
        Profiler.End();
        
        Profiler.Begin("AnimeStats seed from orchestrator");
        Console.WriteLine("Seeding AnimeStats database...");
        await _seedAnimeStats.SeedAnimeStatsAsync();
        Profiler.End();
        
        Profiler.Begin("AnimeRecommendations seed from orchestrator");
        Console.WriteLine("Seeding AnimeRecommendations database...");
        await _seedAnimeRecommendations.SeedAnimeRecommendationsAsync();
        Profiler.End();

        Profiler.Begin("AnimeRatings seed from orchestrator");
        Console.WriteLine("Seeding AnimeRatings database...");
        await _seedAnimeRatings.SeedAnimeRatingsAsync();
        Profiler.End();
    }
}