using System.Globalization;
using CsvHelper;

namespace AnimeProject.Persistence.CSV;

public class CsvAnimeParser
{
    private readonly string _detailsPath;

    public CsvAnimeParser(string detailsPath = "/home/izaya/Desktop/YetAnotherAnimeList/Data/CSVs/details.csv")
    {
        _detailsPath = detailsPath;
    }
    
    public List<RawAnimeDto> Parse()
    {
        using var reader = new StreamReader(_detailsPath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        // Have to turn it into a list so it reads the whole file
        List<RawAnimeDto> details = csv.GetRecords<RawAnimeDto>().ToList();

        return details;
    }
}