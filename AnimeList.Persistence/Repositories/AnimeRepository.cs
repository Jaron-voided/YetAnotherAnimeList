using System.Data;
using AnimeProject.Application.Interfaces;
using AnimeProject.Domain.Enums;
using AnimeProject.Domain.Models;

namespace AnimeProject.Persistence.Repositories;

public class AnimeRepository : IAnimeRepository
{
    private readonly IDbConnection _connection;
    
    public AnimeRepository(IDbConnection connection)
    {
        _connection = connection;
    }
    public Task<IEnumerable<Anime>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Anime?> GetByIdAsync(int malId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Anime>> GetByTitleAsync(string title)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Anime>> GetByMinimumScoreAsync(double minScore)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Anime>> GetByTypeAsync(AnimeEnums.AnimeType type)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Anime>> GetByStatusAsync(AnimeEnums.AnimeStatus status)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Anime>> GetByRatingAsync(AnimeEnums.AnimeRating rating)
    {
        throw new NotImplementedException();
    }
}