using AnimeList.Domain.Models;

namespace AnimeList.Persistence.CSV.AnimeRecommendations;

public class RawAnimeRecommendationsMapper
{
    public Domain.Models.AnimeRecommendations MapNewAnime(
        int baseId, int suggestedId, int count)
    {
        var animeRecommendation = new Domain.Models.AnimeRecommendations
        {
            BaseMalId = baseId,
            SuggestedMalId = suggestedId,
            TimesSuggestedCount = count
        };
        
        return animeRecommendation;
    }
}