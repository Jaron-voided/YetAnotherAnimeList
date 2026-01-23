using AnimeList.Application.DTOs.UserAnimeEntries;
using AnimeList.Application.Mapping.Anime;
using AnimeList.Application.Mapping.UserAnimeEntries;
using AnimeList.Application.RepoInterfaces.Anime;
using AnimeList.Application.RepoInterfaces.UserAnimeEntry;

namespace AnimeList.Application.Handlers.UserAnimeEntry;

public class UserAnimeEntryCommandHandler
{
    private readonly IUserAnimeEntryRepository _userAnimeEntryRepository;
    private readonly IUserAnimeEntryMapper _userAnimeEntryMapper;
    private readonly IAnimeMapper _animeMapper;
    private readonly IAnimeRepository _animeRepository;

    public UserAnimeEntryCommandHandler(IUserAnimeEntryRepository userAnimeEntryRepository,
        IUserAnimeEntryMapper userAnimeEntryMapper, IAnimeRepository animeRepository,
        IAnimeMapper animeMapper)
    {
        _userAnimeEntryRepository = userAnimeEntryRepository;
        _userAnimeEntryMapper = userAnimeEntryMapper;
        _animeMapper = animeMapper;
        _animeRepository = animeRepository;
    }

    public async Task AddEntryAsync(CreateUserAnimeEntryDto createUserAnimeEntryDto)
    {
        var userAnimeEntry = _userAnimeEntryMapper.CreateAnimeEntry(createUserAnimeEntryDto);
        await _userAnimeEntryRepository.AddEntryAsync(userAnimeEntry);
    }    
    
    public async Task UpdateEntryAsync(UpdateUserAnimeEntryDto updateUserAnimeEntryDto)
    {
        var existingUserAnimeEntry = await _userAnimeEntryRepository.GetEntryAsync(updateUserAnimeEntryDto.EntryId);
        var updatedUserAnimeEntry = _userAnimeEntryMapper.UpdateAnimeEntry(updateUserAnimeEntryDto, existingUserAnimeEntry);
        await _userAnimeEntryRepository.UpdateEntryAsync(updatedUserAnimeEntry);
    }    
    
    public async Task DeleteEntryAsync(int entryId)
    {
        await _userAnimeEntryRepository.DeleteEntryAsync(entryId);
    }
}