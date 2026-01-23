using System.Diagnostics;
using System.Runtime.Intrinsics.Arm;
using AnimeList.Application.RepoInterfaces.Anime;
using AnimeList.Application.RepoInterfaces.AnimeRatings;
using AnimeList.Domain.Models;
using AnimeList.Persistence.CSV.AnimeRatings;
using AnimeList.Persistence.Diagnostics;
using AnimeList.Persistence.Repositories.Anime;

namespace AnimeList.Persistence.Database.Orchestrator;

public class SeedAnimeRatings
{
    private readonly IAnimeRatingsLoadRepository _animeRatingsLoadRepository;
    private readonly CsvAnimeRatingsParser _csvAnimeRatingsParser;
    private readonly RawAnimeRatingsMapper _rawAnimeRatingsMapper;
    private readonly AnimeRatingsFilter _animeRatingsFilter;
    private readonly IAnimeRepository _animeRepo;
    private readonly IDbConnectionFactory _connectionFactory;

    public SeedAnimeRatings(
        IAnimeRatingsLoadRepository animeRatingsLoadRepository,
        CsvAnimeRatingsParser csvAnimeRatingsParser,
        RawAnimeRatingsMapper rawAnimeRatingsMapper,
        AnimeRatingsFilter animeRatingsFilter,
        IAnimeRepository animeRepo,
        IDbConnectionFactory connectionFactory
    )
    {
        _animeRatingsLoadRepository = animeRatingsLoadRepository;
        _csvAnimeRatingsParser = csvAnimeRatingsParser;
        _rawAnimeRatingsMapper = rawAnimeRatingsMapper;
        _animeRatingsFilter = animeRatingsFilter;
        _animeRepo = animeRepo;
        _connectionFactory = connectionFactory;
    }

    internal async Task SeedAnimeRatingsAsync()
    {
        Profiler.SetDefaultBufferSize(1024);
        Profiler.Begin("From inside SeedAnimeRatings");
        var sw = Stopwatch.StartNew();
        var idsTask = _animeRepo.GetAllMalIdsAsync();
        var validIds =  await idsTask;

        using var conn = _connectionFactory.CreateConnection();
        
        IEnumerable<RawAnimeRatingsDto> parsedRatings = _csvAnimeRatingsParser.StreamRatings();

        IEnumerable<RawAnimeRatingsDto> filteredRatings = _animeRatingsFilter.Filter(parsedRatings, validIds);

        IEnumerable<AnimeRatings> mappedRatings = _rawAnimeRatingsMapper.MapAll(filteredRatings);

        List<AnimeRatings> animeRatings = new List<AnimeRatings>();
        foreach (var mappedRating in mappedRatings)
        {
            animeRatings.Add(mappedRating);
            
            if (animeRatings.Count == 50000)
            {
                await _animeRatingsLoadRepository.InsertAnimeRatingsBatchAsync(animeRatings, conn);
                animeRatings = new List<AnimeRatings>();
            }
        }

        await _animeRatingsLoadRepository.InsertAnimeRatingsBatchAsync(animeRatings, conn);

        Profiler.End();
        sw.Stop();
        Console.WriteLine($"Total seed pipeline took :  {sw.ElapsedMilliseconds} ms");
    }
    /*internal async Task SeedAnimeRatingsAsync()
    {
        var sw = Stopwatch.StartNew();
        foreach (var r in _csvAnimeRatingsParser.StreamRatings())
        {
            await _animeRatingsLoadRepository.InsertAnimeRatingAsync(_rawAnimeRatingsMapper.Map(r));
        }
    }*/
    /*
    private async Task SeedAnimeRatingsAsync()
    {
        var swParse = Stopwatch.StartNew();

        IEnumerable<RawAnimeRatingsDto> rawAnimeRatingsDtos =
            _csvAnimeRatingsParser.Parse();

        //Console.WriteLine($"Seeding {rawAnimeRatingsDtos.Count()} raw AnimeRatings records...");
        swParse.Stop();

        var swMap = Stopwatch.StartNew();
        IEnumerable<AnimeRatings> animeRatings = MapAllAnimeRatings(rawAnimeRatingsDtos);
        swMap.Stop();

        var swLoad = Stopwatch.StartNew();
        //await _animeRatingsLoadRepository
          //  .InsertAllAnimeRatingsAsync(animeRatings);
          foreach (AnimeRatings rating in animeRatings)
          {
              await _animeRatingsLoadRepository.InsertAnimeRatingAsync(rating);
          }

        swLoad.Stop();

        Console.WriteLine(
            $"Parsing took {swParse.ElapsedMilliseconds} ms \n" +
            $"Aggregation took {swMap.ElapsedMilliseconds} ms \n" +
            $"Loading DB took {swLoad.ElapsedMilliseconds} ms"
        );
    }
    */
    
    private IEnumerable<AnimeRatings> MapAllAnimeRatings(IEnumerable<RawAnimeRatingsDto> rawAnimeRatingsDtos)
    {
        //IEnumerable<AnimeRatings> cleanAnimeRatings = new List<AnimeRatings>();

        foreach (RawAnimeRatingsDto dto in rawAnimeRatingsDtos)
        {
            //yield return cleanAnimeRatings.Add(_rawAnimeRatingsMapper.Map(dto));
            yield return _rawAnimeRatingsMapper.Map(dto);
        }
        
        // return cleanAnimeRatings;
    }    
}