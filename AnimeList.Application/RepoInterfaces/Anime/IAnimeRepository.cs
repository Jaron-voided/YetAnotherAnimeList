using AnimeList.Domain.Enums;

namespace AnimeList.Application.RepoInterfaces.Anime;

public interface IAnimeRepository
{
    Task<IEnumerable<Domain.Models.Anime>> GetAllAsync();
    Task<Domain.Models.Anime?> GetByIdAsync(int malId);
    Task<HashSet<int>> GetAllMalIdsAsync();
    Task<IEnumerable<Domain.Models.Anime>> GetByTitleAsync(string title);
    Task<IEnumerable<Domain.Models.Anime>> GetByMinimumScoreAsync(double minScore);
    Task<IEnumerable<Domain.Models.Anime>> GetByTypeAsync(AnimeEnums.AnimeType type);
    Task<IEnumerable<Domain.Models.Anime>> GetByStatusAsync(AnimeEnums.AnimeStatus status);
    Task<IEnumerable<Domain.Models.Anime>> GetByRatingAsync(AnimeEnums.AnimeRating rating);
    Task<IEnumerable<Domain.Models.Anime>> GetMultipleAnimeByIdAsync(List<int> animeEntryIds);
}