namespace AnimeList.Application.Handlers.DTOs;

public class AnimeDto
{
    public int MalId { get; set; }
    
    public string Title { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;
    
    public string Rating { get; set; } = string.Empty;

    public double? Score { get; set; }

    public DateOnly? StartDate { get; set; }
    
    public DateOnly? EndDate { get; set; }

    public string? Synopsis { get; set; }

    public int? Rank { get; set; }

    public int? Popularity { get; set; }
    
    public string? Genres { get; set; }
    
    public int? Episodes { get; set; }
    
    public int? Year { get; set; }

    public string? Streaming { get; set; }

}
