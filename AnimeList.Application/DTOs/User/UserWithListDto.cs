using AnimeList.Application.DTOs.UserAnimeEntries;

namespace AnimeList.Application.DTOs.User;

public class UserWithListDto
{
    public int UserId { get; set; }
    
    public string? Username { get; set; }

    public List<ReadAnimeEntryDto> ReadAnimeEntries { get; set; } = new();
}