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
        using var reader = new StreamReader(_ratingsPath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        csv.Read();
        csv.ReadHeader();
        
        while (csv.Read())
            yield return csv.GetRecord<RawAnimeRatingsDto>();
    }

    /*public RawAnimeRatingsDto ParseSingleRow(CsvReader csv)
    {
        using var reader = new StreamReader(_ratingsPath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var rating = csv.GetRecord<RawAnimeRatingsDto>();
        
        return rating;
    }*/
}