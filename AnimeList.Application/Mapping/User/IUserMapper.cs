using AnimeList.Application.DTOs.User;
using AnimeList.Application.DTOs.UserAnimeEntries;
using AnimeList.Domain.Models;

namespace AnimeList.Application.Mapping.User;

public interface IUserMapper
{
    Domain.Models.User CreateUser(CreateUserDto dto);
    
    BasicUserDto CreateBasicUserDto(Domain.Models.User user);
    
    UserWithListDto CreateUserWithListDto(Domain.Models.User user, List<ReadAnimeEntryDto> animeEntriesDtos);
}