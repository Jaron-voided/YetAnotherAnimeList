using AnimeList.Domain.Enums;
using AnimeList.Domain.Models;

namespace AnimeList.Tests.Unit.TestData;

public class AnimeTestData
{
    public static readonly Anime Bebop = new()
    {
        MalId = 1,
        Title = "Cowboy Bebop",
        ImageUrl = "https://example.com/db.jpg",
        Type = AnimeEnums.AnimeType.TV,
        Rating = AnimeEnums.AnimeRating.PG13,
        Status = AnimeEnums.AnimeStatus.FinishedAiring,
        StartDate = DateTime.Now
    };
}