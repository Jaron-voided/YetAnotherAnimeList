

namespace AnimeList.Application.Views;

public class AnimeStatsViewDto
{
    public int MalId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public double? Score { get; set; }
    public AnimeStatsDto Stats { get; set; } = new();
}
