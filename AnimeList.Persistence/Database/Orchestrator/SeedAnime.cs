using System.Diagnostics;
using AnimeList.Application.RepoInterfaces.Anime;
using AnimeList.Domain.Models;
using AnimeList.Persistence.CSV.Anime;
using AnimeList.Persistence.Diagnostics;

namespace AnimeList.Persistence.Database.Orchestrator;

public class SeedAnime
{
    private readonly IAnimeLoadRepository _animeLoadRepository;
    private readonly CsvAnimeParser _csvAnimeParser;
    private readonly RawAnimeMapper _rawAnimeMapper;

    public SeedAnime(
        IAnimeLoadRepository animeLoadRepository,
        CsvAnimeParser csvAnimeParser,
        RawAnimeMapper rawAnimeMapper
    )
    {
        _animeLoadRepository = animeLoadRepository;
        _csvAnimeParser = csvAnimeParser;
        _rawAnimeMapper = rawAnimeMapper;
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

    internal async Task SeedAnimeAsync()
    {
        Profiler.SetDefaultBufferSize(1024);
        Profiler.Begin("From inside SeedAnimeAsync");
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
        Profiler.End();
    }
}