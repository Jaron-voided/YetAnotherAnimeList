using System.Globalization;
using CsvHelper;

namespace AnimeList.Persistence.CSV.AnimeRecommendations;

public class CsvAnimeRecommendationsParser
{
    private readonly string _recommendationsPath;
    
    public CsvAnimeRecommendationsParser(string recommendationsPath)
    {
        _recommendationsPath = recommendationsPath;
    }

    public List<RawAnimeRecommendationsDto> Parse()
    {
        using var reader = new StreamReader(_recommendationsPath);
        Console.WriteLine($"CsvAnimeRecommendations recommendationsPath = {_recommendationsPath}");
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        List<RawAnimeRecommendationsDto> recommendations = csv.GetRecords<RawAnimeRecommendationsDto>().ToList();
        Console.WriteLine($"Anime Recommendations length = {recommendations.Count()}");
        
        return recommendations;
    }
}