using AnimeList.Application.DTOs.User;
using AnimeList.Application.Interfaces.Anime;
using AnimeList.Application.Mapping.Anime;
using AnimeList.Application.Mapping.User;
using AnimeList.Application.Mapping.UserAnimeEntries;
using AnimeList.Application.RepoInterfaces.User;
using AnimeList.Application.RepoInterfaces.UserAnimeEntry;

namespace AnimeList.Application.Handlers.User;

public class UserCommandHandler
{
    private readonly IAnimeRepository _animeRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUserAnimeEntryRepository _userAnimeEntryRepository;
    
    private readonly IAnimeMapper _animeMapper; 
    private readonly IUserAnimeEntryMapper _userAnimeEntryMapper;
    private readonly IUserMapper _userMapper;

    public UserCommandHandler(IAnimeRepository animeRepository, IUserRepository userRepository,
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
    
    public async Task CreateUserAsync(CreateUserDto createUserDto)
    {
        var user = _userMapper.CreateUser(createUserDto);
        await _userRepository.CreateUserAsync(user);
    }

    public async Task DeleteUserAsync(int userId)
    {
        await _userRepository.DeleteUserAsync(userId);
    }
}