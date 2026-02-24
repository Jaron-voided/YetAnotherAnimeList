using AnimeList.Application.RepoInterfaces.Anime;
using AnimeList.Application.RepoInterfaces.AnimeRatings;
using AnimeList.Application.RepoInterfaces.AnimeRecommendations;
using AnimeList.Application.RepoInterfaces.AnimeStats;
 using AnimeList.Application.TasteRecommendations;
using AnimeList.Application.Views;
using AnimeList.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeList.Application.Handlers.TasteRecommendations;

public class TasteQueryHandler
{
    private readonly IAnimeRecommendationsRepository _recommendationsRepo;
    private readonly IAnimeRepository _animeRepo;
    private readonly IAnimeStatsRepository _statsRepo;
    private readonly IAnimeRatingsRepository _tasteRepo;

    public TasteQueryHandler(IAnimeRecommendationsRepository recommendationsRepo, IAnimeRepository animeRepo, IAnimeStatsRepository statsRepo, IAnimeRatingsRepository tasteRepo)
    {
        _recommendationsRepo = recommendationsRepo;
        _animeRepo = animeRepo;
        _statsRepo = statsRepo;
        _tasteRepo = tasteRepo;
    }

    public async Task<TasteRecommendationViewDto> GetTasteRecommendationsAsync(TasteRecommendationRequestDto request)
    {
        // Obtain similar users
        var users = await _tasteRepo.GetSimilarUsersAsync(request.MalIds, request.MinScore, request.MinCommonAnime);
        
        // Obtain those users highly rated anime I have not watched
        var recommendedAnime = await _tasteRepo.GetRecommendedAnimesAsync(users, request.MalIds, request.MinScore, request.MaxRecommendations);

        // Prepare data to transform into TasteRecommendationView
        int seedCount = request.MalIds.Count();
        int similarUserCount = users.Count();
        List<TasteRecommendationItemDto> recommendedAnimeList = new List<TasteRecommendationItemDto>();

        // Obtain a list of AnimeIds to query my Anime table with
        IEnumerable<int> animeIds = recommendedAnime.Select(a => a.MalId).ToList();

        // Get the card items from anime
        IEnumerable<AnimeCardReadModel> animeCards = await _animeRepo.GetAnimeRecommendationItemsByIdsAsync(animeIds);

        var recommendedItems = recommendedAnime
            .Join(animeCards,
                r => r.MalId,
                a => a.MalId,
                (r, a) => new TasteRecommendationItemDto
                {
                    MalId = a.MalId,
                    Title = a.Title,
                    ImageUrl = a.ImageUrl,
                    Score = a.Score,
                    RecommendationCount = r.RecommendationCount
                })
            .ToList();

        return new TasteRecommendationViewDto
        {
            SeedCount = seedCount,
            SimilarUserCount = similarUserCount,
            Recommendations = recommendedItems
        };
/*        foreach(var anime in recommendedAnime)
        {
            AnimeCardReadModel anime = await _animeRepo.GetAnimeRecommendationItemsByIdsAsync
        }*/
    }
}
