using AnimeList.Application.Interfaces.Anime;

namespace AnimeList.Persistence.CSV.AnimeRatings;

public class AnimeRatingsFilter
{
    private readonly IAnimeRepository _animeRepository;

    public AnimeRatingsFilter(IAnimeRepository animeRepository)
    {
        _animeRepository = animeRepository;
    }

    internal void EnsureValidId(RawAnimeRatingsDto animeRatingDto,  out RawAnimeRatingsDto validAnimeRatingDto)
    {
        
    }
}