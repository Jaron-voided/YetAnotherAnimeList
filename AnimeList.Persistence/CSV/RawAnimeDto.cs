using CsvHelper.Configuration.Attributes;

namespace AnimeList.Persistence.CSV;

public class RawAnimeDto
{
    [Name("mal_id")]
    public int MalId { get; set; }

    [Name("title")]
    public string Title { get; set; } = string.Empty;

    [Name("title_japanese")]
    public string? TitleJapanese { get; set; }

    [Name("url")]
    public string Url { get; set; } = string.Empty;

    [Name("image_url")]
    public string ImageUrl { get; set; } = string.Empty;

    [Name("type")]
    public string? RawType { get; set; }

    [Name("status")]
    public string? RawStatus { get; set; }

    [Name("score")]
    public double? Score { get; set; }

    [Name("scored_by")]
    public double? ScoredBy { get; set; }

    [Name("start_date")]
    public DateTime? StartDate { get; set; }

    [Name("end_date")]
    public DateTime? EndDate { get; set; }

    [Name("synopsis")]
    public string? Synopsis { get; set; }

    [Name("rank")]
    public double? Rank { get; set; }

    [Name("popularity")]
    public int? Popularity { get; set; }

    [Name("members")]
    public int? Members { get; set; }

    [Name("favorites")]
    public int? Favorites { get; set; }

    [Name("genres")]
    public string? Genres { get; set; }

    [Name("studios")]
    public string? Studios { get; set; }

    [Name("themes")]
    public string? Themes { get; set; }

    [Name("demographics")]
    public string? Demographics { get; set; }

    [Name("source")]
    public string? Source { get; set; }

    [Name("rating")]
    public string? RawRating { get; set; }

    [Name("episodes")]
    public double? Episodes { get; set; }

    [Name("season")]
    public string? Season { get; set; }

    [Name("year")]
    public double? Year { get; set; }

    [Name("producers")]
    public string? Producers { get; set; }

    [Name("explicit_genres")]
    public string? ExplicitGenres { get; set; }

    [Name("licensors")]
    public string? Licensors { get; set; }

    [Name("streaming")]
    public string? Streaming { get; set; }
}