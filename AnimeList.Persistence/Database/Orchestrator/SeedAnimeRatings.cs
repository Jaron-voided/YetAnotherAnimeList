using System.Diagnostics;
using AnimeList.Application.RepoInterfaces.AnimeRatings;
using AnimeList.Domain.Models;
using AnimeList.Persistence.CSV.AnimeRatings;

namespace AnimeList.Persistence.Database.Orchestrator;

public class SeedAnimeRatings
{
    private readonly IAnimeRatingsLoadRepository _animeRatingsLoadRepository;
    private readonly CsvAnimeRatingsParser _csvAnimeRatingsParser;
    private readonly RawAnimeRatingsMapper _rawAnimeRatingsMapper;

    public SeedAnimeRatings(
        IAnimeRatingsLoadRepository animeRatingsLoadRepository,
        CsvAnimeRatingsParser csvAnimeRatingsParser,
        RawAnimeRatingsMapper rawAnimeRatingsMapper
    )
    {
        _animeRatingsLoadRepository = animeRatingsLoadRepository;
        _csvAnimeRatingsParser = csvAnimeRatingsParser;
        _rawAnimeRatingsMapper = rawAnimeRatingsMapper;
    }
    
    internal async Task SeedAnimeRatingsAsync()
    {
        var sw = Stopwatch.StartNew();
        foreach (var r in _csvAnimeRatingsParser.StreamRatings())
        {
            await _animeRatingsLoadRepository.InsertAnimeRatingAsync(_rawAnimeRatingsMapper.Map(r));
        }
    }
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