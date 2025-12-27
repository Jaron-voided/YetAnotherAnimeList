using AnimeList.Domain.Enums;

namespace AnimeList.Application.RepoInterfaces.AnimeRatings;

public interface IAnimeRatingsLoadRepository
{
    Task<bool> HasBeenLoadedAsync();
    Task InsertAnimeRatingAsync(Domain.Models.AnimeRatings animeRating);
    Task InsertAllAnimeRatingsAsync(IEnumerable<Domain.Models.AnimeRatings> animeRatings);
}