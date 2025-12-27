using CsvHelper.Configuration.Attributes;

namespace AnimeList.Persistence.CSV.AnimeRatings;

public class RawAnimeRatingsDto
{
    [Name("username")]
    public string Username {get; set;}
    
    [Name("anime_id")]
    public int MalId {get; set;}
    
    [Name("status")]
    public string RawStatus {get; set;}
    
    [Name("score")]
    public int Score {get; set;}
    
    [Name("is_rewatching")]
    public double? IsRewatching {get; set;}
    
    [Name("num_watched_episodes")]
    public int NumWatchedEpisodes {get; set;}
}