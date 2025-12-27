using AnimeList.Domain.Enums;

namespace AnimeList.Application.DTOs.UserAnimeEntries;

public class ReadAnimeEntryDto
{
    public int EntryId { get; set; }
    
    public int UserId { get; set; }
    
    public int MalId { get; set; }
    
    public double? Rating { get; set; }
    
    public string? Comment { get; set; }
    
    public int? CurrentSeason { get; set; }
    
    public int? CurrentEpisode { get; set; }
    
    public UserAnimeEnums.WatchStatus? WatchStatus { get; set; }
     
    public UserAnimeEnums.RewatchStatus? RewatchStatus { get; set; }

    
    // Items from the anime

    public string? Title { get; set; }
    
    public string? ImageUrl { get; set; }
}