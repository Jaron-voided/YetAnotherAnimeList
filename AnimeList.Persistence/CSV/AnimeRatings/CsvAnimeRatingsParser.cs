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

    public List<RawAnimeRatingsDto> Parse()
    {
        using var reader = new StreamReader(_ratingsPath);

        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        
        List<RawAnimeRatingsDto> ratings = csv.GetRecords<RawAnimeRatingsDto>().ToList();

        return ratings;
    }
}