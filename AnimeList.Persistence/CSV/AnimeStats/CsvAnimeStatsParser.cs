using AnimeList.Persistence.Diagnostics;
using CsvHelper;
using System.Globalization;

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
        Profiler.SetDefaultBufferSize(1024);
        Profiler.Begin("Stats parser from CsvAnimeStatsParser");
        using var reader = new StreamReader(_statsPath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        List<RawAnimeStatsDto> stats = csv.GetRecords<RawAnimeStatsDto>().ToList();

        Profiler.End();
        return stats;
    }
}