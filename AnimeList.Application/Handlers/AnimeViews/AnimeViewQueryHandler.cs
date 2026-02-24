using AnimeList.Application.RepoInterfaces.Anime;
using AnimeList.Application.RepoInterfaces.AnimeRecommendations;
using AnimeList.Application.RepoInterfaces.AnimeStats;
using AnimeList.Application.Views;
using AnimeList.Domain.ViewModels;


namespace AnimeList.Application.Handlers.AnimeViews;

public class AnimeViewQueryHandler
{
    private readonly IAnimeRecommendationsRepository _recommendationsRepo;
    private readonly IAnimeRepository _animeRepo;
    private readonly IAnimeStatsRepository _statsRepo;
     
    public AnimeViewQueryHandler(IAnimeRepository animeRepo, 
        IAnimeRecommendationsRepository recommendationsRepo,
        IAnimeStatsRepository statsRepository)
    {
        _recommendationsRepo = recommendationsRepo;
        _animeRepo = animeRepo;
        _statsRepo = statsRepository;
    }

    public async Task<AnimeCardReadModel> GetAnimeItemViewAsync(int id)
    {
        var animeItemReadModel =
            await _animeRepo.GetAnimeRecommendationItemByIdAsync(id);

        return animeItemReadModel;
    }
    public async Task<AnimeRecommendationsViewDto> GetAnimeRecommendationsViewAsync(int id)
    {
        var suggestedIds =
            (await _recommendationsRepo.GetAllSuggestedIdsForBaseAsync(id))
            .Distinct()
            .ToList();

        var suggestedAnimeReadModels =
            await _animeRepo.GetAnimeRecommendationItemsByIdsAsync(suggestedIds);

        var suggestedAnimeDtos = suggestedAnimeReadModels.Select(r => new AnimeItemDto
        {
            MalId = r.MalId,
            Title = r.Title,
            ImageUrl = r.ImageUrl,
            Score = r.Score
        }).ToList();

        var baseAnimeDto =
            await _animeRepo.GetAnimeRecommendationItemByIdAsync(id);

        return new AnimeRecommendationsViewDto
        {
            MalId = baseAnimeDto.MalId,
            Title = baseAnimeDto.Title,
            ImageUrl = baseAnimeDto.ImageUrl,
            Score = baseAnimeDto.Score,
            Recommendations = suggestedAnimeDtos
        };   
    }

    public async Task<AnimeStatsViewDto> GetAnimeStatsViewAsync(int id)
    {
        var baseAnimeDto =
            await _animeRepo.GetAnimeRecommendationItemByIdAsync(id);

        var stats = 
            await _statsRepo.GetAnimeStatsByIdAsync(id);

        var statsDto = new AnimeStatsDto
        {
            Watching = stats.Watching,
            Completed = stats.Completed,
            OnHold = stats.OnHold,
            Dropped = stats.Dropped,
            PlanToWatch = stats.PlanToWatch,
            Total = stats.Total,
            Score8Votes = stats.Score8Votes,
            Score9Votes = stats.Score9Votes,
            Score10Votes = stats.Score10Votes
        };


        return new AnimeStatsViewDto
        {
            MalId = baseAnimeDto.MalId,
            Title = baseAnimeDto.Title,
            ImageUrl = baseAnimeDto.ImageUrl,
            Score = baseAnimeDto.Score,
            Stats = statsDto
        };
    }
}
