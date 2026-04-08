using AnimeList.Persistence.Diagnostics;
using CsvHelper;
using System.Globalization;

namespace AnimeList.Persistence.CSV.Anime;

public class CsvAnimeParser
{
    private readonly string _detailsPath;

    public CsvAnimeParser(string detailsPath)
    {
        _detailsPath = detailsPath;
    }
    
    public List<RawAnimeDto> Parse()
    {
        Profiler.SetDefaultBufferSize(1024);
        Profiler.Begin("Anime Parser");

        using var reader = new StreamReader(_detailsPath);
        Console.WriteLine($"CsvAnimeParser detailsPath =  {_detailsPath}");
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        // Have to turn it into a list so it reads the whole file
        List<RawAnimeDto> details = csv.GetRecords<RawAnimeDto>().ToList();
        Console.WriteLine($"Anime Details length =  {details.Count}");

        Profiler.End();
        return details;
    }
}