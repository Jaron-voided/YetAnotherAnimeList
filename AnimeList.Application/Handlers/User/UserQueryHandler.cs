using AnimeList.Application.DTOs.Anime;
using AnimeList.Application.DTOs.User;
using AnimeList.Application.DTOs.UserAnimeEntries;
using AnimeList.Application.Mapping.Anime;
using AnimeList.Application.Mapping.User;
using AnimeList.Application.Mapping.UserAnimeEntries;
using AnimeList.Application.RepoInterfaces.Anime;
using AnimeList.Application.RepoInterfaces.User;
using AnimeList.Application.RepoInterfaces.UserAnimeEntry;
using AnimeList.Domain.Models;

namespace AnimeList.Application.Handlers.User;

public class UserQueryHandler
{
    private readonly IAnimeRepository _animeRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUserAnimeEntryRepository _userAnimeEntryRepository;
    
    private readonly IAnimeMapper _animeMapper; 
    private readonly IUserAnimeEntryMapper _userAnimeEntryMapper;
    private readonly IUserMapper _userMapper;

    public UserQueryHandler(IAnimeRepository animeRepository, IUserRepository userRepository,
        IUserAnimeEntryRepository userAnimeEntryRepository,
        IAnimeMapper animeMapper, IUserAnimeEntryMapper userAnimeEntryMapper, IUserMapper userMapper)
    {
        _animeRepository = animeRepository;
        _userRepository = userRepository;
        _userAnimeEntryRepository = userAnimeEntryRepository;
        _animeMapper = animeMapper;
        _userMapper = userMapper;
        _userAnimeEntryMapper = userAnimeEntryMapper;
    }
    

    public async Task<BasicUserDto?> GetUserByIdAsync(int userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);

        if (user is null)
            return null;

        return _userMapper.CreateBasicUserDto(user);
    }    
    public async Task<BasicUserDto?> GetUserByUsernameAsync(string userName)
    {
        var user = await _userRepository.GetUserByUsernameAsync(userName);

        if (user is null)
            return null;

        return _userMapper.CreateBasicUserDto(user);
    }
    
    // Get all the AnimeEntries from a user in the form of a ReadAnimeEntryDto
    public async Task<IEnumerable<ReadAnimeEntryDto>> GetUserReadAnimeEntriesAsync(int userId)
    {
        // Return a list of anime entries for user
        List<Domain.Models.UserAnimeEntry> animeEntries = 
            (await _userAnimeEntryRepository.GetAllEntriesOfUserAsync(userId)).ToList();

        // build a list of IDs for these anime entries
        List<int> animeEntryIds = animeEntries
            .Select(entry => entry.MalId)
            .Distinct()
            .ToList();
        
        if (animeEntryIds.Count == 0)
            return Enumerable.Empty<ReadAnimeEntryDto>();
        
        // Use these IDs to build a list of actual anime
        // TODO figure out of this is the best way to achieve results
        IEnumerable<Domain.Models.Anime> animeList = await _animeRepository.GetMultipleAnimeByIdAsync(animeEntryIds);
        
        // Turn these anime into dtos, which I need to run my ReadAnimeEntry Mapper
        IEnumerable<AnimeDto> animeDtoList = _animeMapper.ToDtoList(animeList);
        
        // Build a lookup (malID -> AnimeDTO) So we can look up entries by ID
        Dictionary<int, AnimeDto> animeLookup = animeDtoList.ToDictionary(animeDto => animeDto.MalId);

        // Create an empty list to fill and return
        List<ReadAnimeEntryDto> readAnimeResults = new List<ReadAnimeEntryDto>(animeEntries.Count);

        // For each AnimeEntry grab the matching DTO and map to ReadAnimeEntryDTO
        foreach (var animeEntry in animeEntries)
        {
            // if the anime dictionary does not contain the value, skip to the next loop
            // if it does return AnimeDto
            if (!animeLookup.TryGetValue(animeEntry.MalId, out AnimeDto animeDto))
                continue;
            
            // Transform to ReadAnimeEntryDTO (AnimeEntry plus Anime.Name/ImageURL
            ReadAnimeEntryDto mappedAnime = _userAnimeEntryMapper.ReadAnimeEntry(animeEntry, animeDto);
            readAnimeResults.Add(mappedAnime);
        }
        
        return readAnimeResults;
    }
}