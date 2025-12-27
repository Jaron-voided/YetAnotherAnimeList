namespace AnimeList.Application.RepoInterfaces.AnimeRecommendations;

public interface IAnimeRecommendationsLoadRepository
{
    Task<bool> HasBeenLoadedAsync();
    //Task InsertAnimeRecommendationsAsync(Domain.Models.AnimeRecommendations animeRecommendations);
    Task InsertAllAnimeRecommendationsAsync(IEnumerable<Domain.Models.AnimeRecommendations> animeRecommendations);
}