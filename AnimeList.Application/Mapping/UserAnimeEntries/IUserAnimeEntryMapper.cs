using AnimeList.Application.DTOs.Anime;
using AnimeList.Application.DTOs.UserAnimeEntries;
using AnimeList.Domain.Models;

namespace AnimeList.Application.Mapping.UserAnimeEntries;

public interface IUserAnimeEntryMapper
{
    UserAnimeEntry CreateAnimeEntry(CreateUserAnimeEntryDto dto);
    
    UserAnimeEntry UpdateAnimeEntry(UpdateUserAnimeEntryDto dto, UserAnimeEntry anime);
    
    ReadAnimeEntryDto ReadAnimeEntry(UserAnimeEntry animeEntry, AnimeDto animeDto);
}