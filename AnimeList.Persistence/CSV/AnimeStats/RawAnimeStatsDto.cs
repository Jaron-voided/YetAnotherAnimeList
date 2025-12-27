using CsvHelper.Configuration.Attributes;

namespace AnimeList.Persistence.CSV.AnimeStats;

public class RawAnimeStatsDto
{
    [Name("mal_id")]
    public int MalId { get; set; }

    [Name("watching")]
    public int Watching { get; set; }

    [Name("completed")]
    public int Completed { get; set; }

    [Name("on_hold")]
    public int OnHold { get; set; }

    [Name("dropped")]
    public int Dropped { get; set; }

    [Name("plan_to_watch")]
    public int PlanToWatch { get; set; }

    [Name("total")]
    public int Total { get; set; }

    [Name("score_1_votes")]
    public double? Score1Votes { get; set; }

    [Name("score_1_percentage")]
    public double? Score1Percentage { get; set; }

    [Name("score_2_votes")]
    public double? Score2Votes { get; set; }

    [Name("score_2_percentage")]
    public double? Score2Percentage { get; set; }

    [Name("score_3_votes")]
    public double? Score3Votes { get; set; }

    [Name("score_3_percentage")]
    public double? Score3Percentage { get; set; }

    [Name("score_4_votes")]
    public double? Score4Votes { get; set; }

    [Name("score_4_percentage")]
    public double? Score4Percentage { get; set; }

    [Name("score_5_votes")]
    public double? Score5Votes { get; set; }

    [Name("score_5_percentage")]
    public double? Score5Percentage { get; set; }

    [Name("score_6_votes")]
    public double? Score6Votes { get; set; }

    [Name("score_6_percentage")]
    public double? Score6Percentage { get; set; }

    [Name("score_7_votes")]
    public double? Score7Votes { get; set; }

    [Name("score_7_percentage")]
    public double? Score7Percentage { get; set; }

    [Name("score_8_votes")]
    public double? Score8Votes { get; set; }

    [Name("score_8_percentage")]
    public double? Score8Percentage { get; set; }

    [Name("score_9_votes")]
    public double? Score9Votes { get; set; }

    [Name("score_9_percentage")]
    public double? Score9Percentage { get; set; }

    [Name("score_10_votes")]
    public double? Score10Votes { get; set; }

    [Name("score_10_percentage")]
    public double? Score10Percentage { get; set; }
}