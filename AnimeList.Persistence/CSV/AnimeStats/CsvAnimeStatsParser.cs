using System.Globalization;
using CsvHelper;

namespace AnimeList.Persistence.CSV.AnimeStats;

public class CsvAnimeStatsParser
{
    private readonly string _statsPath;

    public CsvAnimeStatsParser(string statsPath)
    {
        _statsPath = statsPath;
    }

    public List<RawAnimeStatsDto> Parse()
    {
        using var reader = new StreamReader(_statsPath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        List<RawAnimeStatsDto> stats = csv.GetRecords<RawAnimeStatsDto>().ToList();

        return stats;
    }
}