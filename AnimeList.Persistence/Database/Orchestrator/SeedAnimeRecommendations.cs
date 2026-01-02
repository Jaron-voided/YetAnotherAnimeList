using System.Diagnostics;
using AnimeList.Application.Interfaces.Anime;
using AnimeList.Application.RepoInterfaces.AnimeRecommendations;
using AnimeList.Domain.Models;
using AnimeList.Persistence.CSV.AnimeRecommendations;

namespace AnimeList.Persistence.Database.Orchestrator;

public class SeedAnimeRecommendations
{
    private readonly IAnimeRecommendationsLoadRepository _animeRecommendationsLoadRepository;
    private readonly CsvAnimeRecommendationsParser _csvAnimeRecommendationsParser;
    //private readonly RawAnimeRecommendationsMapper _rawAnimeRecommendationsMapper;
    private readonly RawAnimeRecommendationsAggregator _animeRecommendationsAggregator;
    
    private readonly IAnimeRepository _animeRepository;


    public SeedAnimeRecommendations(
        IAnimeRecommendationsLoadRepository animeRecommendationsLoadRepository,
        CsvAnimeRecommendationsParser csvAnimeRecommendationsParser,
        //RawAnimeRecommendationsMapper animeRecommendationsMapper,
        RawAnimeRecommendationsAggregator animeRecommendationsAggregator,
        IAnimeRepository animeRepository
    )
    {
        _animeRecommendationsLoadRepository = animeRecommendationsLoadRepository;
        _csvAnimeRecommendationsParser = csvAnimeRecommendationsParser;
        //_rawAnimeRecommendationsMapper = animeRecommendationsMapper;
        _animeRecommendationsAggregator = animeRecommendationsAggregator;
        _animeRepository = animeRepository;
    }
    
    internal async Task SeedAnimeRecommendationsAsync()
    {
        var swParse = Stopwatch.StartNew();

        IEnumerable<RawAnimeRecommendationsDto> rawAnimeRecommendationsDtos =
            _csvAnimeRecommendationsParser.Parse();
        
        Console.WriteLine($"Seeding {rawAnimeRecommendationsDtos.Count()} raw AnimeRecommendations records...");
        
        var prunedAnimeRecommendations = await PruneAnimeRecommendations(rawAnimeRecommendationsDtos);
        //Console.WriteLine($"Pruned down to {prunedAnimeRecommendations.Count()} raw AnimeRecommendations records...");
        swParse.Stop();
        
        var swAggregate = Stopwatch.StartNew();
        List<AnimeRecommendations> aggregatedRecommendations =
            _animeRecommendationsAggregator.AggregateAnimeRecommendations(prunedAnimeRecommendations);
        swAggregate.Stop();

        Console.WriteLine($"Aggregated into {aggregatedRecommendations.Count} unique AnimeRecommendations records");

    
        var swLoad = Stopwatch.StartNew();
        await _animeRecommendationsLoadRepository
            .InsertAllAnimeRecommendationsAsync(aggregatedRecommendations);
        swLoad.Stop();

        Console.WriteLine(
            $"Parsing took {swParse.ElapsedMilliseconds} ms \n" +
            $"Aggregation took {swAggregate.ElapsedMilliseconds} ms \n" +
            $"Loading DB took {swLoad.ElapsedMilliseconds} ms"
        );
    }

    private async Task<IEnumerable<RawAnimeRecommendationsDto>> PruneAnimeRecommendations(
        IEnumerable<RawAnimeRecommendationsDto> rawAnimeRecommendationsDtos)
    {
        var idsTask = _animeRepository.GetAllMalIdsAsync();
        HashSet<int> validIds = await idsTask;

        var prunedResults = rawAnimeRecommendationsDtos.Where(dto =>
            validIds.Contains(dto.BaseMalId) &&
            validIds.Contains(dto.SuggestedMalId));

        return prunedResults.ToList();
    }

}