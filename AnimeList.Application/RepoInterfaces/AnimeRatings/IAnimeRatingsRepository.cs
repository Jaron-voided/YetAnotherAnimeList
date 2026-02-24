using AnimeList.Application.TasteRecommendations;

namespace AnimeList.Application.RepoInterfaces.AnimeRatings;

public interface IAnimeRatingsRepository
{
    Task<IReadOnlyList<string>> GetSimilarUsersAsync(IEnumerable<int> malIds, int minScore, int minCommonAnime/*TasteRecommendationRequestDto request*/);

    Task<IReadOnlyList<RecommendedAnime>> GetRecommendedAnimesAsync(IEnumerable<string> users, IEnumerable<int> seedIds, int minScore, int maxRecommendations/*TasteRecommendationRequestDto request*/);
   
    
    
    /*Task<IEnumerable<Domain.Models.AnimeRatings>> GetRatingsForUserAsync(string username, int limit);

    Task<IEnumerable<Domain.Models.AnimeRatings>> GetRatingsForAnimeAsync(int malId, int limit);
    *//// In the future I can maybe do
    /// GetTopRatedForUser
    /// MostRatedAnime
    /// HighestRatedAnime??
}