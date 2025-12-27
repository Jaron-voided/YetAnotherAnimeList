namespace AnimeList.Persistence.CSV.AnimeStats;

public class RawAnimeStatsMapper
{
    public Domain.Models.AnimeStats Map(RawAnimeStatsDto rawStats)
    {
        var modelAnimeStats = new Domain.Models.AnimeStats
        {
            MalId = rawStats.MalId,
            Watching = rawStats.Watching,
            Completed = rawStats.Completed,
            OnHold = rawStats.OnHold,
            Dropped = rawStats.Dropped,
            PlanToWatch = rawStats.PlanToWatch,
            Total = rawStats.Total,
            Score8Votes = rawStats.Score8Votes,
            Score9Votes = rawStats.Score9Votes,
            Score10Votes = rawStats.Score10Votes
        };
        
        return modelAnimeStats;
    }
}