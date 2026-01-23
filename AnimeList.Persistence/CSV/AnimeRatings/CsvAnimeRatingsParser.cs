using System.Diagnostics;
using System.Globalization;
using CsvHelper;

namespace AnimeList.Persistence.CSV.AnimeRatings;

public class CsvAnimeRatingsParser
{
    private readonly string _ratingsPath;
    
    public CsvAnimeRatingsParser(string ratingsPath)
    {
        _ratingsPath = ratingsPath;
    }

    public IEnumerable<RawAnimeRatingsDto> Parse()
    {
        using var reader = new StreamReader(_ratingsPath);

        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        
        IEnumerable<RawAnimeRatingsDto> ratings = csv.GetRecords<RawAnimeRatingsDto>();

        return ratings;
    }

    public IEnumerable<RawAnimeRatingsDto> StreamRatings()
    {
        var sw = new Stopwatch();
        sw.Start();
        int count = 0;
        using var reader = new StreamReader(_ratingsPath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        csv.Read();
        csv.ReadHeader();

        while (csv.Read())
        {
            count += 1;
            yield return csv.GetRecord<RawAnimeRatingsDto>();
        }
        sw.Stop();
        Console.WriteLine("Parsed rows = "  + count);
        Console.WriteLine($"Elapsed time for parsing: {sw.Elapsed}");
    }

    /*public RawAnimeRatingsDto ParseSingleRow(CsvReader csv)
    {
        using var reader = new StreamReader(_ratingsPath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var rating = csv.GetRecord<RawAnimeRatingsDto>();
        
        return rating;
    }*/
}