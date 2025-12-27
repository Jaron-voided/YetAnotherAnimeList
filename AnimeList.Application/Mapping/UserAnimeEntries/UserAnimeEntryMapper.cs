using AnimeList.Application.DTOs.Anime;
using AnimeList.Application.DTOs.UserAnimeEntries;
using AnimeList.Domain.Models;

namespace AnimeList.Application.Mapping.UserAnimeEntries;

public class UserAnimeEntryMapper :  IUserAnimeEntryMapper
{
    public UserAnimeEntry CreateAnimeEntry(CreateUserAnimeEntryDto dto)
    {
        return new UserAnimeEntry
        {
            UserId = dto.UserId,
            MalId = dto.MalId,
            Rating = dto.Rating,
            Comment = dto.Comment,
            CurrentSeason = dto.CurrentSeason,
            CurrentEpisode = dto.CurrentEpisode,
            WatchStatus = dto.WatchStatus,
            RewatchStatus = dto.RewatchStatus
        };
    }

    public UserAnimeEntry UpdateAnimeEntry(UpdateUserAnimeEntryDto dto, UserAnimeEntry animeEntry)
    {
        animeEntry.Rating = dto.Rating ?? animeEntry.Rating;
        animeEntry.Comment = dto.Comment ?? animeEntry.Comment;
        animeEntry.CurrentSeason = dto.CurrentSeason ?? animeEntry.CurrentSeason;
        animeEntry.CurrentEpisode = dto.CurrentEpisode ?? animeEntry.CurrentEpisode;
        animeEntry.WatchStatus = dto.WatchStatus ?? animeEntry.WatchStatus;
        animeEntry.RewatchStatus = dto.RewatchStatus ?? animeEntry.RewatchStatus;

        return animeEntry;
    }

    public ReadAnimeEntryDto ReadAnimeEntry(UserAnimeEntry animeEntry, AnimeDto animeDto)
    {
        return new ReadAnimeEntryDto
        {
            EntryId = animeEntry.EntryId,
            UserId = animeEntry.UserId,
            MalId = animeEntry.MalId,
            Rating = animeEntry.Rating,
            Comment = animeEntry.Comment,
            CurrentSeason = animeEntry.CurrentSeason,
            CurrentEpisode = animeEntry.CurrentEpisode,
            WatchStatus = animeEntry.WatchStatus,
            RewatchStatus = animeEntry.RewatchStatus,
            Title = animeDto.Title,
            ImageUrl = animeDto.ImageUrl
        };
    }
}