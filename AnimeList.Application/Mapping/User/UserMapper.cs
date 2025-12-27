using AnimeList.Application.DTOs.Anime;
using AnimeList.Application.DTOs.User;
using AnimeList.Application.DTOs.UserAnimeEntries;
using AnimeList.Application.Handlers.Anime.Query;
using AnimeList.Application.Mapping.UserAnimeEntries;
using AnimeList.Domain.Models;

namespace AnimeList.Application.Mapping.User;

public class UserMapper : IUserMapper
{
    private readonly IUserAnimeEntryMapper _userAnimeEntryMapper;


    public UserMapper(IUserAnimeEntryMapper userAnimeEntryMapper )
    {
        _userAnimeEntryMapper = userAnimeEntryMapper;
    }
    
    public Domain.Models.User CreateUser(CreateUserDto dto)
    {
        return new Domain.Models.User
        {
            Username = dto.Username,
            Email = dto.Email,
            UserAnimeEntries = new List<UserAnimeEntry>()
        };
    }

    public BasicUserDto CreateBasicUserDto(Domain.Models.User user)
    {
        return new BasicUserDto
        {
            UserId = user.UserId,
            Username = user.Username,
            Email = user.Email,
        };
    }

    public UserWithListDto CreateUserWithListDto(Domain.Models.User user, List<ReadAnimeEntryDto> animeEntriesDtos)
    {
        return new UserWithListDto
        {
            UserId = user.UserId,
            Username = user.Username,
            ReadAnimeEntries = animeEntriesDtos
        };
    }
}