using AnimeList.Domain.Enums;

namespace AnimeList.Domain.Models;

public class Anime
{
    public int MalId { get; set; }
    
    public string Title { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;

    public AnimeEnums.AnimeType Type { get; set; }

    public AnimeEnums.AnimeStatus Status { get; set; }
    
    public AnimeEnums.AnimeRating Rating { get; set; }

    public double? Score { get; set; }

    public DateTime? StartDate { get; set; }
    
    public DateTime? EndDate { get; set; }

    public string? Synopsis { get; set; }

    public int? Rank { get; set; }

    public int? Popularity { get; set; }
    
    public string? Genres { get; set; }
    
    public int? Episodes { get; set; }
    
    public int? Year { get; set; }

    public string? Streaming { get; set; }
}