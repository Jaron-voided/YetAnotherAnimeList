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
        Console.WriteLine("Seeding Anime database...");
        await _seedAnime.SeedAnimeAsync();
        
        Console.WriteLine("Seeding AnimeStats database...");
        await _seedAnimeStats.SeedAnimeStatsAsync();
        
        Console.WriteLine("Seeding AnimeRecommendations database...");
        await _seedAnimeRecommendations.SeedAnimeRecommendationsAsync();
        
        Console.WriteLine("Seeding AnimeRatings database...");
        await _seedAnimeRatings.SeedAnimeRatingsAsync();
    }
}