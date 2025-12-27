using CsvHelper.Configuration.Attributes;

namespace AnimeList.Persistence.CSV.AnimeRecommendations;

public class RawAnimeRecommendationsDto
{
    [Name("mal_id")]
    public int BaseMalId {get; set;}
    
    [Name("recommendation_mal_id")]
    public int SuggestedMalId {get; set;}
}