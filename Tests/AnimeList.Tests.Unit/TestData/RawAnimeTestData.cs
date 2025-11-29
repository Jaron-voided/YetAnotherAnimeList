using AnimeList.Persistence.CSV;

namespace AnimeList.Tests.Unit.TestData;

public static class RawAnimeTestData
{
    public static readonly RawAnimeDto Bebop = new()
    {
        MalId = 1,
        Title = "Cowboy Bebop",
        ImageUrl = "https://example.com/db.jpg",
        RawType = "TV",
        RawRating = "PG-13",
        RawStatus = "Finished Airing"
    };

    public static readonly RawAnimeDto Trigun = new()
    {
        MalId = 2,
        Title = "Trigun",
        ImageUrl = "https://example2.com/db.jpg",
        RawType = "Movie",
        RawRating = "R",
        RawStatus = "Finished Airing"
    };
}