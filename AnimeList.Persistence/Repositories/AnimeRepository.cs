using System.Data;
using AnimeProject.Application.Interfaces;
using AnimeProject.Domain.Enums;
using AnimeProject.Domain.Models;
using AnimeProject.Persistence.Database;
using Dapper;

namespace AnimeProject.Persistence.Repositories;

public class AnimeRepository : IAnimeRepository
{
    private readonly IDbConnectionFactory _connectionFactory;
    
    public AnimeRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    public async Task<IEnumerable<Anime>> GetAllAsync()
    {
        using var connection = _connectionFactory.CreateConnection();

        const string sql = "SELECT * FROM Anime";
        return await connection.QueryAsync<Anime>(sql);
    }

    public Task<Anime?> GetByIdAsync(int malId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Anime>> GetByTitleAsync(string title)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Anime>> GetByMinimumScoreAsync(double minScore)
    {
        using var connection = _connectionFactory.CreateConnection();
        
        const string sql = @"SELECT * 
                            FROM Anime 
                            WHERE score >= @MinScore";

        return await connection.QueryAsync<Anime>(sql, new { MinScore = minScore });
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