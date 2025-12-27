namespace AnimeList.Domain.Models;

public class User
{
    public int UserId { get; set; }
    
    public string? Username { get; set; }
    
    public string? Email { get; set; }
    
    public List<UserAnimeEntry> UserAnimeEntries { get; set; } = new List<UserAnimeEntry>();
}