using AnimeList.Domain.Enums;

namespace AnimeList.Domain.Models;

public class AnimeRatings
{
    public string Username { get; set; }

    public int MalId { get; set; }
    
    public UserAnimeEnums.WatchStatus Status { get; set; }
    
    public int Score { get; set; }
    
    public int NumberOfWatchedEpisodes { get; set; }

}