namespace AnimeList.Application.RepoInterfaces.AnimeRatings;

public interface IAnimeRatingsRepository
{
    Task<IEnumerable<Domain.Models.AnimeRatings>> GetRatingsForUserAsync(string username, int limit);

    Task<IEnumerable<Domain.Models.AnimeRatings>> GetRatingsForAnimeAsync(int malId, int limit);
    /// In the future I can maybe do
    /// GetTopRatedForUser
    /// MostRatedAnime
    /// HighestRatedAnime??
}