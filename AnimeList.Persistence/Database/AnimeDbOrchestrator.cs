using System.Collections;
using System.Diagnostics;
using AnimeList.Application.DTOs.Anime;
using AnimeList.Application.Interfaces;
using AnimeList.Application.Interfaces.Anime;
using AnimeList.Application.RepoInterfaces.Anime;
using AnimeList.Application.RepoInterfaces.AnimeRatings;
using AnimeList.Application.RepoInterfaces.AnimeRecommendations;
using AnimeList.Application.RepoInterfaces.AnimeStats;
using AnimeList.Domain.Models;
using AnimeList.Persistence.CSV;
using AnimeList.Persistence.CSV.Anime;
using AnimeList.Persistence.CSV.AnimeRatings;
using AnimeList.Persistence.CSV.AnimeRecommendations;
using AnimeList.Persistence.CSV.AnimeStats;

namespace AnimeList.Persistence.Database;

public class AnimeDbOrchestrator
{
    private readonly IAnimeLoadRepository _animeLoadRepository;
    private readonly CsvAnimeParser _csvAnimeParser;
    private readonly RawAnimeMapper _rawAnimeMapper; 
    
    private readonly IAnimeStatsLoadRepository _animeStatsLoadRepository;
    private readonly CsvAnimeStatsParser _csvAnimeStatsParser;
    private readonly RawAnimeStatsMapper _rawAnimeStatsMapper;
    
    private readonly IAnimeRecommendationsLoadRepository _animeRecommendationsLoadRepository;
    private readonly CsvAnimeRecommendationsParser _csvAnimeRecommendationsParser;
    private readonly RawAnimeRecommendationsMapper _rawAnimeRecommendationsMapper;
    private readonly RawAnimeRecommendationsAggregator _animeRecommendationsAggregator;
    
    private readonly IAnimeRepository _animeRepository;

    private readonly IAnimeRatingsLoadRepository _animeRatingsLoadRepository;
    private readonly CsvAnimeRatingsParser _csvAnimeRatingsParser;
    private readonly RawAnimeRatingsMapper _rawAnimeRatingsMapper;
    

    public AnimeDbOrchestrator(
        IAnimeLoadRepository animeLoadRepository,
        CsvAnimeParser csvAnimeParser,
        RawAnimeMapper rawAnimeMapper, 
        IAnimeStatsLoadRepository animeStatsLoadRepository,
        CsvAnimeStatsParser csvAnimeStatsParser, 
        RawAnimeStatsMapper rawAnimeStatsMapper,
        IAnimeRecommendationsLoadRepository animeRecommendationsLoadRepository,
        CsvAnimeRecommendationsParser csvAnimeRecommendationsParser,
        RawAnimeRecommendationsMapper animeRecommendationsMapper,
        RawAnimeRecommendationsAggregator animeRecommendationsAggregator,
        IAnimeRepository animeRepository,
        IAnimeRatingsLoadRepository animeRatingsLoadRepository,
        CsvAnimeRatingsParser csvAnimeRatingsParser,
        RawAnimeRatingsMapper rawAnimeRatingsMapper
        )
    {
        _animeLoadRepository = animeLoadRepository;
        _csvAnimeParser = csvAnimeParser;
        _rawAnimeMapper = rawAnimeMapper;
        _animeStatsLoadRepository = animeStatsLoadRepository;
        _csvAnimeStatsParser = csvAnimeStatsParser;
        _rawAnimeStatsMapper = rawAnimeStatsMapper;
        _animeRecommendationsLoadRepository = animeRecommendationsLoadRepository;
        _csvAnimeRecommendationsParser = csvAnimeRecommendationsParser;
        _rawAnimeRecommendationsMapper = animeRecommendationsMapper;
        _animeRecommendationsAggregator = animeRecommendationsAggregator;
        _animeRepository = animeRepository;
        _animeRatingsLoadRepository = animeRatingsLoadRepository;
        _csvAnimeRatingsParser = csvAnimeRatingsParser;
        _rawAnimeRatingsMapper = rawAnimeRatingsMapper;
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
        await SeedAnimeAsync();
        
        Console.WriteLine("Seeding AnimeStats database...");
        await SeedAnimeStatsAsync();
        
        Console.WriteLine("Seeding AnimeRecommendations database...");
        await SeedAnimeRecommendationsAsync();
        
        Console.WriteLine("Seeding AnimeRatings database...");
        await SeedAnimeRatingsAsync();
    }
    
    private IEnumerable<Anime> MapAllAnimes(IEnumerable<RawAnimeDto> rawAnimeDtos)
    {
        Console.WriteLine($"Mapping {rawAnimeDtos.Count()} raw Anime records");
        var cleanAnimes = new List<Anime>();
        
        foreach (RawAnimeDto dto in rawAnimeDtos)
        {
            cleanAnimes.Add(_rawAnimeMapper.Map(dto));
        }
        
        Console.WriteLine($"Mapped  {cleanAnimes.Count()} clean Anime records");
        
        return cleanAnimes;
    }

    private async Task SeedAnimeAsync()
    {
        var swParse = Stopwatch.StartNew();

        IEnumerable<RawAnimeDto> rawAnimeDtos = _csvAnimeParser.Parse();
        Console.WriteLine($"Seeding {rawAnimeDtos.Count()} raw Anime records...");
        swParse.Stop();

        
        var swMap = Stopwatch.StartNew();
        var cleanAnimes = MapAllAnimes(rawAnimeDtos);
        Console.WriteLine($"Seeding {cleanAnimes.Count()} clean Anime records");
        swMap.Stop();

        
        var swLoad = Stopwatch.StartNew();
        await _animeLoadRepository.InsertAllAnimeAsync(cleanAnimes);
        swLoad.Stop();

        Console.WriteLine($"Parsing took {swParse.ElapsedMilliseconds} ms \n, " +
                          $"Mapping took {swMap.ElapsedMilliseconds} ms \n, " +
                          $"Loading DB took {swLoad.ElapsedMilliseconds} ms"); 
    }
    
    private async Task SeedAnimeStatsAsync()
    {
        var swParse = Stopwatch.StartNew();

        IEnumerable<RawAnimeStatsDto> rawAnimeStatsDtos = _csvAnimeStatsParser.Parse();
        Console.WriteLine($"Seeding {rawAnimeStatsDtos.Count()} animeStats");
        swParse.Stop();
        
        var swMap = Stopwatch.StartNew();
        var cleanAnimeStats = MapAllAnimeStats(rawAnimeStatsDtos);
        swMap.Stop();

        
        var swLoad = Stopwatch.StartNew();
        await _animeStatsLoadRepository.InsertAllAnimeStatsAsync(cleanAnimeStats);
        swLoad.Stop();

        Console.WriteLine($"Parsing took {swParse.ElapsedMilliseconds} ms \n, " +
                          $"Mapping took {swMap.ElapsedMilliseconds} ms \n, " +
                          $"Loading DB took {swLoad.ElapsedMilliseconds} ms"); 
    }

    private IEnumerable<AnimeStats> MapAllAnimeStats(IEnumerable<RawAnimeStatsDto> rawAnimeStatsDtos)
    {
        var cleanAnimeStats = new List<AnimeStats>();

        foreach (RawAnimeStatsDto dto in rawAnimeStatsDtos)
        {
            cleanAnimeStats.Add(_rawAnimeStatsMapper.Map(dto));
        }
        
        return cleanAnimeStats;
    }    
    
    private async Task SeedAnimeRecommendationsAsync()
    {
        var swParse = Stopwatch.StartNew();

        IEnumerable<RawAnimeRecommendationsDto> rawAnimeRecommendationsDtos =
            _csvAnimeRecommendationsParser.Parse();
        
        Console.WriteLine($"Seeding {rawAnimeRecommendationsDtos.Count()} raw AnimeRecommendations records...");
        
        var prunedAnimeRecommendations = await PruneAnimeRecommendations(rawAnimeRecommendationsDtos);
        Console.WriteLine($"Pruned down to {prunedAnimeRecommendations.Count()} raw AnimeRecommendations records...");
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
    
    private async Task SeedAnimeRatingsAsync()
    {
        var swParse = Stopwatch.StartNew();

        IEnumerable<RawAnimeRatingsDto> rawAnimeRatingsDtos =
            _csvAnimeRatingsParser.Parse();
        
        Console.WriteLine($"Seeding {rawAnimeRatingsDtos.Count()} raw AnimeRatings records...");
        swParse.Stop();
        
        var swMap = Stopwatch.StartNew();
        IEnumerable<AnimeRatings> animeRatings = MapAllAnimeRatings(rawAnimeRatingsDtos);
        swMap.Stop();
        
        var swLoad = Stopwatch.StartNew();
        await _animeRatingsLoadRepository
            .InsertAllAnimeRatingsAsync(animeRatings);
        swLoad.Stop();

        Console.WriteLine(
            $"Parsing took {swParse.ElapsedMilliseconds} ms \n" +
            $"Aggregation took {swMap.ElapsedMilliseconds} ms \n" +
            $"Loading DB took {swLoad.ElapsedMilliseconds} ms"
        );
    }
    
    private IEnumerable<AnimeRatings> MapAllAnimeRatings(IEnumerable<RawAnimeRatingsDto> rawAnimeRatingsDtos)
    {
        var cleanAnimeRatings = new List<AnimeRatings>();

        foreach (RawAnimeRatingsDto dto in rawAnimeRatingsDtos)
        {
            cleanAnimeRatings.Add(_rawAnimeRatingsMapper.Map(dto));
        }
        
        return cleanAnimeRatings;
    }    
}