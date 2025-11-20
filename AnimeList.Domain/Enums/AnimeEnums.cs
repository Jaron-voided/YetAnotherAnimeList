namespace AnimeProject.Domain.Enums;

public class AnimeEnums
{
    public enum AnimeType
    {
        Movie,
        Music,
        OVA,
        Special,
        TV,
        ONA,
        CM,
        TVSpecial,
        PV,
        Unknown
    }

    public enum AnimeStatus
    {
        FinishedAiring,
        CurrentlyAiring,
        NotYetAired,
        Unknown
    }

    public enum AnimeRating
    {
        G,
        PG,
        PG13,
        R,
        RPlus,
        Rx,
        Unknown
    }
}