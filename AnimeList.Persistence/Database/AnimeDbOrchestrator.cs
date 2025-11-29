using AnimeList.Application.Interfaces;
using AnimeList.Domain.Models;
using AnimeList.Persistence.CSV;

namespace AnimeList.Persistence.Database;

public class AnimeDbOrchestrator
{
    private readonly IAnimeLoadRepository _animeLoadRepository;
    private readonly CsvAnimeParser _csvAnimeParser;
    private readonly RawAnimeMapper _rawAnimeMapper;

    public AnimeDbOrchestrator(
        IAnimeLoadRepository animeLoadRepository,
        CsvAnimeParser csvAnimeParser,
        RawAnimeMapper rawAnimeMapper)
    {
        _animeLoadRepository = animeLoadRepository;
        _csvAnimeParser = csvAnimeParser;
        _rawAnimeMapper = rawAnimeMapper;
    }

    public async Task SeedDatabaseAsync()
    {
        List<RawAnimeDto> rawAnimeDtos = _csvAnimeParser.Parse();
        
        List<Anime> cleanAnimes = MapAllAnimes(rawAnimeDtos);

        await _animeLoadRepository.InsertAllAnimeAsync(cleanAnimes);
    }
    
    // It keeps suggesting IEnumerable?
    private List<Anime> MapAllAnimes(List<RawAnimeDto> rawAnimeDtos)
    {
        List<Anime> cleanAnimes = new List<Anime>();
        
        foreach (RawAnimeDto dto in rawAnimeDtos)
        {
            cleanAnimes.Add(_rawAnimeMapper.Map(dto));
        }
        
        return cleanAnimes;
    }
}