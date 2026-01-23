using System.Data;
using AnimeList.Domain.Enums;

namespace AnimeList.Application.RepoInterfaces.AnimeRatings;

public interface IAnimeRatingsLoadRepository
{
    Task<bool> HasBeenLoadedAsync();
    Task InsertAnimeRatingAsync(Domain.Models.AnimeRatings animeRating);
    Task InsertAnimeRatingsBatchAsync(IReadOnlyList<Domain.Models.AnimeRatings> animeRatings, IDbConnection conn);
    Task InsertAllAnimeRatingsAsync(IEnumerable<Domain.Models.AnimeRatings> animeRatings);
}