using AnimeList.Domain.Models;

namespace AnimeList.Persistence.CSV.AnimeRecommendations;

public class RawAnimeRecommendationsAggregator
{
    private RawAnimeRecommendationsMapper _mapper;

    public RawAnimeRecommendationsAggregator(RawAnimeRecommendationsMapper mapper)
    {
        _mapper = mapper;
    }

    public Dictionary<int, Dictionary<int, int>> CreateRecommendationsDictionary(
        IEnumerable<RawAnimeRecommendationsDto> rawAnimeRecommendationsDtos)
    {
        Dictionary<int, Dictionary<int, int>> animeRecommendationsDictionary = new Dictionary<int, Dictionary<int, int>>();

        foreach (RawAnimeRecommendationsDto dto in rawAnimeRecommendationsDtos)
        {
            if (!animeRecommendationsDictionary.TryGetValue(dto.BaseMalId, out var inner))
            {
                inner = new Dictionary<int, int>();
                animeRecommendationsDictionary[dto.BaseMalId] = inner;
            }

            if (!inner.TryAdd(dto.SuggestedMalId, 1))
            {
                inner[dto.SuggestedMalId]++;
            }
        }
        
        return animeRecommendationsDictionary;
    }

    public List<Domain.Models.AnimeRecommendations> AggregateAnimeRecommendations(
        IEnumerable<RawAnimeRecommendationsDto> rawAnimeRecommendationsDtos)
    {
        Dictionary<int, Dictionary<int, int>> animeRecommendationsDictionary =
            CreateRecommendationsDictionary(rawAnimeRecommendationsDtos);

        List<Domain.Models.AnimeRecommendations> animeRecommendationsList =
            new List<Domain.Models.AnimeRecommendations>();

        foreach (var (baseId, innerDict) in animeRecommendationsDictionary)
        {
            foreach (var (suggestedId, count) in innerDict)
            {
                Domain.Models.AnimeRecommendations animeRecommendation =  _mapper.MapNewAnime(baseId, suggestedId, count);
                animeRecommendationsList.Add(animeRecommendation);
            }
        }
        
        return  animeRecommendationsList;
    }
}