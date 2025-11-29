using AnimeList.Domain.Enums;
using AnimeList.Domain.Models;

namespace AnimeList.Application.Interfaces;

public interface IAnimeRepository
{
    Task<IEnumerable<Anime>> GetAllAsync();
    Task<Anime?> GetByIdAsync(int malId);
    Task<IEnumerable<Anime>> GetByTitleAsync(string title);
    Task<IEnumerable<Anime>> GetByMinimumScoreAsync(double minScore);
    Task<IEnumerable<Anime>> GetByTypeAsync(AnimeEnums.AnimeType type);
    Task<IEnumerable<Anime>> GetByStatusAsync(AnimeEnums.AnimeStatus status);
    Task<IEnumerable<Anime>> GetByRatingAsync(AnimeEnums.AnimeRating rating);
}