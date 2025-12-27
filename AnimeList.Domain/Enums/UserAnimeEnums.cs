namespace AnimeList.Domain.Enums;

public class UserAnimeEnums
{
    public enum WatchStatus
    {
        Watching,
        Completed,
        WaitingForNewRelease,
        OnHold,
        Dropped,
        PlanToWatch,
        Unknown
    }

    public enum RewatchStatus
    {
        NotRewatching,
        PlanToRewatch,
        Rewatching,
        CompletedRewatch
    }
}