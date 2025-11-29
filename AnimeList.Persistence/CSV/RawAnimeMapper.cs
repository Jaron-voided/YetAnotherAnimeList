using AnimeList.Domain.Enums;
using AnimeList.Domain.Models;

namespace AnimeList.Persistence.CSV;

public class RawAnimeMapper
{
    public Anime Map(RawAnimeDto rawDetails)
    {
        var modelAnime = new Anime
        {
            MalId = rawDetails.MalId,
            Title = rawDetails.Title ?? string.Empty,
            ImageUrl = rawDetails.ImageUrl ?? string.Empty,
            
            Type = MapType(rawDetails),
            Status = MapStatus(rawDetails),
            Rating = MapRating(rawDetails),
            
            Score = rawDetails.Score,
            StartDate = rawDetails.StartDate,
            EndDate = rawDetails.EndDate,
            
            Synopsis = rawDetails.Synopsis,
            
            Rank = rawDetails.Rank.HasValue
                ? (int)rawDetails.Rank.Value
                : null,
            
            Popularity = rawDetails.Popularity,
            Genres = rawDetails.Genres,
            
            Episodes = rawDetails.Episodes.HasValue
                ? (int)rawDetails.Episodes
                : null,
            
            Year = rawDetails.Year.HasValue
                ? (int)rawDetails.Year
                : null,
            
            Streaming = rawDetails.Streaming
        };
        
        return modelAnime;
    }

    private AnimeEnums.AnimeType MapType(RawAnimeDto dto)
    {
        switch (dto.RawType?.Trim())
        {
            case "TV":
                return AnimeEnums.AnimeType.TV;
            case "Movie":
                return AnimeEnums.AnimeType.Movie;
            case "OVA":
                return AnimeEnums.AnimeType.OVA;
            case "ONA":
                return AnimeEnums.AnimeType.ONA;
            case "Special":
                return AnimeEnums.AnimeType.Special;
            case "TV Special":
                return AnimeEnums.AnimeType.TVSpecial;
            case "Music":
                return AnimeEnums.AnimeType.Music;
            case "PV":
                return AnimeEnums.AnimeType.PV;
            case "CM":
                return AnimeEnums.AnimeType.CM;
        }
        return AnimeEnums.AnimeType.Unknown;
    }
    
    private static AnimeEnums.AnimeStatus MapStatus(RawAnimeDto dto)
    {
        switch (dto.RawStatus?.Trim())
        {
            case "Finished Airing":
                return AnimeEnums.AnimeStatus.FinishedAiring;
            case "Currently Airing":
                return AnimeEnums.AnimeStatus.CurrentlyAiring;
            case "Not yet aired":
                return AnimeEnums.AnimeStatus.NotYetAired;
        }
        return AnimeEnums.AnimeStatus.Unknown;
    }

    private static AnimeEnums.AnimeRating MapRating(RawAnimeDto dto)
    {
        var rating = dto.RawRating?.Trim() ?? string.Empty;

        if (rating.StartsWith("PG-13"))
            return AnimeEnums.AnimeRating.PG13;
        else if (rating.StartsWith("R+"))
            return AnimeEnums.AnimeRating.RPlus;
        else if (rating.StartsWith("Rx"))
            return AnimeEnums.AnimeRating.Rx;
        else if (rating.StartsWith("R"))
            return AnimeEnums.AnimeRating.R;
        else if (rating.StartsWith("PG"))
            return AnimeEnums.AnimeRating.PG;
        else if (rating.StartsWith("G"))
            return AnimeEnums.AnimeRating.G;
        else
            return AnimeEnums.AnimeRating.Unknown;
    }
    
}