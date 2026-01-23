using System.Diagnostics;
using AnimeList.Application.RepoInterfaces.AnimeStats;
using AnimeList.Domain.Models;
using AnimeList.Persistence.CSV.AnimeStats;
using AnimeList.Persistence.Diagnostics;

namespace AnimeList.Persistence.Database.Orchestrator;

public class SeedAnimeStats
{
    private readonly IAnimeStatsLoadRepository _animeStatsLoadRepository;
    private readonly CsvAnimeStatsParser _csvAnimeStatsParser;
    private readonly RawAnimeStatsMapper _rawAnimeStatsMapper;

    public SeedAnimeStats(
        IAnimeStatsLoadRepository animeStatsLoadRepository,
        CsvAnimeStatsParser csvAnimeStatsParser,
        RawAnimeStatsMapper rawAnimeStatsMapper
    )
    {
        _animeStatsLoadRepository = animeStatsLoadRepository;
        _csvAnimeStatsParser = csvAnimeStatsParser;
        _rawAnimeStatsMapper = rawAnimeStatsMapper;
    }
    
    internal async Task SeedAnimeStatsAsync()
    {
        Profiler.SetDefaultBufferSize(1024);
        Profiler.Begin("From SeedAnimeStats");
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

        Profiler.End();
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

}