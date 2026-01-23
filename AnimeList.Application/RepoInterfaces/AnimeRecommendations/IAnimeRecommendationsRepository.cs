using AnimeList.Domain.Projections;

namespace AnimeList.Application.RepoInterfaces.AnimeRecommendations;

public interface IAnimeRecommendationsRepository
{
    Task<IEnumerable<Domain.Models.AnimeRecommendations>> GetAllAsync();
    Task<bool> ExistsAsync(int baseMalId, int suggestedMalId);
    Task<IEnumerable<AnimeRecommendationProjections.RecommendationEdge>> GetRecommendationsForBaseAnimeAsync(int baseMalId, int limit);
    Task<IEnumerable<AnimeRecommendationProjections.RecommendationEdge>> GetRecommendationsForMultipleBaseAnimeAsync(IEnumerable<int> baseMalIds, int perBaseLimit);

    Task<IEnumerable<AnimeRecommendationProjections.RecommendationEdge>> GetBasesForSuggestedAnime(int suggestedMalId);
}