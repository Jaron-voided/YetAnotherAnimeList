using AnimeList.Application.DTOs.User;
using AnimeList.Application.DTOs.UserAnimeEntries;
using AnimeList.Domain.Models;

namespace AnimeList.Application.RepoInterfaces.UserAnimeEntry;

public interface IUserAnimeEntryRepository
{
    Task AddEntryAsync(Domain.Models.UserAnimeEntry animeEntry);
    Task UpdateEntryAsync(Domain.Models.UserAnimeEntry animeEntry);
    Task DeleteEntryAsync(int entryId);
    Task<Domain.Models.UserAnimeEntry?> GetEntryAsync(int entryId);
    // TODO figure out if this is the best way to get multiple entries
    /*Task<IEnumerable<Domain.Models.UserAnimeEntry>> GetMultipleAnimeEntriesAsync(List<int> animeIds);
    Task<IEnumerable<Domain.Models.UserAnimeEntry>> GetMultipleAnimeEntriesAsync(int userId, List<int> animeIds);*/
    Task<IEnumerable<Domain.Models.UserAnimeEntry?>> GetAllEntriesOfUserAsync(int userId);
}