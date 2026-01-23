namespace AnimeList.Domain.Projections;

public static class AnimeRecommendationProjections
{
    public readonly record struct Suggestion(
        int SuggestedMalId,
        int TimesSuggested
    );

    // This mirrors AnimeRecommendaiton, but for handlers the naming
    // might come in handy with readability??
    public readonly record struct RecommendationEdge(
        int BaseMalId,
        int SuggestedMalId,
        int TimesSuggested
    );
}