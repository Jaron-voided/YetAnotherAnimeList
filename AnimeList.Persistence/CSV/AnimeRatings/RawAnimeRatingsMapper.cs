using System.Diagnostics;
using AnimeList.Domain.Enums;

namespace AnimeList.Persistence.CSV.AnimeRatings;

public class RawAnimeRatingsMapper
{

    public Domain.Models.AnimeRatings Map(RawAnimeRatingsDto ratingsDto)
    {
        var modelAnimeRating = new Domain.Models.AnimeRatings
        {
            Username = ratingsDto.Username,
            MalId = ratingsDto.MalId,
            Status = MapStatus(ratingsDto),
            Score = ratingsDto.Score,
            NumberOfWatchedEpisodes = ratingsDto.NumWatchedEpisodes
        };
        
        return modelAnimeRating;
    }

    public IEnumerable<Domain.Models.AnimeRatings> MapAll(IEnumerable<RawAnimeRatingsDto> ratingsDtos)
    {
        var sw = new Stopwatch();
        sw.Start();
        int count = 0;

        foreach (var animeRatingDto in ratingsDtos)
            yield return Map(animeRatingDto);
            
        sw.Stop();
        Console.WriteLine("Parsed rows = "  + count);
        Console.WriteLine($"Elapsed time for mapping: {sw.Elapsed}");
    }

    private UserAnimeEnums.WatchStatus MapStatus(RawAnimeRatingsDto dto)
    {
        switch (dto.RawStatus?.Trim())
        {
            case "Watching":
                return UserAnimeEnums.WatchStatus.Watching;
            case "Completed":
                return UserAnimeEnums.WatchStatus.Completed;
            case "WaitingForNewRelease":
                return UserAnimeEnums.WatchStatus.WaitingForNewRelease;
            case "OnHold":
                return UserAnimeEnums.WatchStatus.OnHold;
            case "Dropped":
                return UserAnimeEnums.WatchStatus.Dropped;
            case "PlanToWatch":
                return UserAnimeEnums.WatchStatus.PlanToWatch;
        }

        return UserAnimeEnums.WatchStatus.Unknown;
    }
}