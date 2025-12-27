namespace AnimeList.Domain.Models;

public class AnimeRecommendations
{
    public int BaseMalId {get; set;}
    
    public int SuggestedMalId { get; set; }
    
    public int TimesSuggestedCount { get; set; }
}