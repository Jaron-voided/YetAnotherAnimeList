using AnimeList.Application.Handlers.DTOs;
using AnimeList.Application.Interfaces;
using AnimeList.Application.Mapping;
using AnimeList.Domain.Enums;

namespace AnimeList.Application.Handlers.Anime.Query;

public class AnimeQueryHandler
{
    private readonly IAnimeRepository _repo;
    private readonly IAnimeMapper _mapper;

    public AnimeQueryHandler(IAnimeRepository repo, IAnimeMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AnimeDto>> GetAllAnimeAsync()
    {
        var models = await _repo.GetAllAsync();
        
        return _mapper.ToDtoList(models);;
    }

    public async Task<AnimeDto?> GetAnimeByIdAsync(int id)
    {
        var model = await _repo.GetByIdAsync(id);

        if (model is null)
            return null;
        
        return _mapper.ToDto(model);
    }

    public async Task<IEnumerable<AnimeDto>> GetAnimeByTitleAsync(string title)
    {
        var models = await _repo.GetByTitleAsync(title);

        return _mapper.ToDtoList(models);
    }

    public async Task<IEnumerable<AnimeDto>> GetAnimeByMinimumScoreAsync(double minScore)
    {
        var models = await _repo.GetByMinimumScoreAsync(minScore);
        
        return _mapper.ToDtoList(models);
    }

    public async Task<IEnumerable<AnimeDto>> GetAnimeByTypeAsync(AnimeEnums.AnimeType type)
    {
        var models = await _repo.GetByTypeAsync(type);
        
        return _mapper.ToDtoList(models);
    }

    public async Task<IEnumerable<AnimeDto>> GetAnimeByStatusAsync(AnimeEnums.AnimeStatus status)
    {
        var models = await _repo.GetByStatusAsync(status);
        
        return _mapper.ToDtoList(models);
    }

    public async Task<IEnumerable<AnimeDto>> GetAnimeByRatingAsync(AnimeEnums.AnimeRating rating)
    {
        var models = await _repo.GetByRatingAsync(rating);
        
        return _mapper.ToDtoList(models);
    }

    /*// optional future:
    public async Task<IEnumerable<AnimeDto>> FilterAsync(
        AnimeEnums.AnimeType? type = null,
        AnimeEnums.AnimeStatus? status = null,
        AnimeEnums.AnimeRating? rating = null,
        double? minScore = null)
    {
        throw new NotImplementedException();
    }*/
}