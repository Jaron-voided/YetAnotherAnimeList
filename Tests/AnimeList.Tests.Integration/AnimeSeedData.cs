using System;
using System.Collections.Generic;
using AnimeList.Domain.Enums;
using AnimeList.Domain.Models;

namespace AnimeList.Tests.TestData;

public static class AnimeSeedData
{
    public static IReadOnlyList<Anime> FiveAnimes() => new List<Anime>
    {
        new Anime
        {
            MalId = 820,
            Title = "Ginga Eiyuu Densetsu",
            ImageUrl = "https://cdn.myanimelist.net/images/anime/1976/142016.jpg",
            Type = AnimeEnums.AnimeType.OVA,
            Status = AnimeEnums.AnimeStatus.FinishedAiring,
            Rating = AnimeEnums.AnimeRating.R,
            Score = 9.02,
            StartDate = new DateTime(1988, 1, 7),
            EndDate = new DateTime(1997, 3, 16),
            Synopsis = "Interstellar war between the Galactic Empire and the Free Planets Alliance led by Reinhard von Lohengramm and Yang Wenli.",
            Rank = 9,
            Popularity = 752,
            Genres = "Drama,Sci-Fi",
            Episodes = 110,
            Year = 1988,
            Streaming = "[]"
        },
        new Anime
        {
            MalId = 5114,
            Title = "Fullmetal Alchemist: Brotherhood",
            ImageUrl = "https://cdn.myanimelist.net/images/anime/1208/94745.jpg",
            Type = AnimeEnums.AnimeType.TV,
            Status = AnimeEnums.AnimeStatus.FinishedAiring,
            Rating = AnimeEnums.AnimeRating.R,
            Score = 9.10,
            StartDate = new DateTime(2009, 4, 4),
            EndDate = new DateTime(2010, 7, 3),
            Synopsis = "Two brothers seek the Philosopher's Stone after a failed alchemy experiment costs them their bodies.",
            Rank = 2,
            Popularity = 3,
            Genres = "Action,Adventure,Drama,Fantasy",
            Episodes = 64,
            Year = 2009,
            Streaming = "[Crunchyroll]"
        },
        new Anime
        {
            MalId = 9253,
            Title = "Steins;Gate",
            ImageUrl = "https://cdn.myanimelist.net/images/anime/1935/127974.jpg",
            Type = AnimeEnums.AnimeType.TV,
            Status = AnimeEnums.AnimeStatus.FinishedAiring,
            Rating = AnimeEnums.AnimeRating.PG13,
            Score = 9.07,
            StartDate = new DateTime(2011, 4, 5),
            EndDate = new DateTime(2011, 9, 13),
            Synopsis = "A self-proclaimed mad scientist discovers time travel and battles the consequences of altering timelines.",
            Rank = 3,
            Popularity = 14,
            Genres = "Drama,Sci-Fi,Suspense",
            Episodes = 24,
            Year = 2011,
            Streaming = "[Crunchyroll,Netflix]"
        },
        new Anime
        {
            MalId = 11061,
            Title = "Hunter x Hunter (2011)",
            ImageUrl = "https://cdn.myanimelist.net/images/anime/1337/99013.jpg",
            Type = AnimeEnums.AnimeType.TV,
            Status = AnimeEnums.AnimeStatus.FinishedAiring,
            Rating = AnimeEnums.AnimeRating.PG13,
            Score = 9.03,
            StartDate = new DateTime(2011, 10, 1),
            EndDate = new DateTime(2014, 9, 23),
            Synopsis = "Young Gon Freecss becomes a Hunter to find his father and uncover the world’s secrets.",
            Rank = 7,
            Popularity = 8,
            Genres = "Action,Adventure,Fantasy",
            Episodes = 148,
            Year = 2011,
            Streaming = "[Crunchyroll,Netflix,Shahid]"
        },
        new Anime
        {
            MalId = 52991,
            Title = "Sousou no Frieren",
            ImageUrl = "https://cdn.myanimelist.net/images/anime/1015/138006.jpg",
            Type = AnimeEnums.AnimeType.TV,
            Status = AnimeEnums.AnimeStatus.FinishedAiring,
            Rating = AnimeEnums.AnimeRating.PG13,
            Score = 9.29,
            StartDate = new DateTime(2023, 9, 28),
            EndDate = new DateTime(2024, 3, 21),
            Synopsis = "An immortal elf mage reflects on loss, time, and human connections after the hero’s journey ends.",
            Rank = 1,
            Popularity = 129,
            Genres = "Adventure,Drama,Fantasy",
            Episodes = 28,
            Year = 2023,
            Streaming = "[Crunchyroll,Netflix]"
        }
    };
}
