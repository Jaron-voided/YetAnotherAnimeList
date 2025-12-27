/*using AnimeList.Application.Interfaces.Anime;
using AnimeList.Application.Mapping.Anime;
using AnimeList.Application.Mapping.UserAnimeEntries;
using AnimeList.Application.RepoInterfaces.UserAnimeEntry;

namespace AnimeList.Application.Handlers.UserAnimeEntry;

public class UserAnimeEntryQueryHandler
{
    private readonly IUserAnimeEntryRepository _userAnimeEntryRepository;
    private readonly IUserAnimeEntryMapper _userAnimeEntryMapper;
    private readonly IAnimeMapper _animeMapper;
    private readonly IAnimeRepository _animeRepository;

    public UserAnimeEntryQueryHandler(IUserAnimeEntryRepository userAnimeEntryRepository,
        IUserAnimeEntryMapper userAnimeEntryMapper, IAnimeRepository animeRepository,
        IAnimeMapper animeMapper)
    {
        _userAnimeEntryRepository = userAnimeEntryRepository;
        _userAnimeEntryMapper = userAnimeEntryMapper;
        _animeMapper = animeMapper;
        _animeRepository = animeRepository;
    }

    public async Task<IEnumerable<> GetAllEntriesOfUserAsync(int userId)
    {
        
    }
}*/