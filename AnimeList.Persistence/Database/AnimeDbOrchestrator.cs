using AnimeProject.Application.Interfaces;
using AnimeProject.Domain.Models;
using AnimeProject.Persistence.CSV;

namespace AnimeProject.Persistence.Database;

public class AnimeDbOrchestrator
{
    private readonly IAnimeRepository _animeRepository;
    private readonly CsvAnimeParser _csvAnimeParser;
    private readonly RawAnimeMapper _rawAnimeMapper;

    public AnimeDbOrchestrator(
        IAnimeRepository animeRepository,
        CsvAnimeParser csvAnimeParser,
        RawAnimeMapper rawAnimeMapper)
    {
        _animeRepository = animeRepository;
        _csvAnimeParser = csvAnimeParser;
        _rawAnimeMapper = rawAnimeMapper;
    }

    public async Task SeedDatabaseAsync()
    {
        List<RawAnimeDto> rawAnimeDtos = _csvAnimeParser.Parse();
        
        List<Anime> cleanAnimes = MapAllAnimes(rawAnimeDtos);

        await _animeRepository.InsertAllAnimeAsync(cleanAnimes);
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