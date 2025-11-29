using System.Data;
using AnimeList.Application.Interfaces;
using AnimeList.Domain.Enums;
using AnimeList.Domain.Models;
using AnimeList.Persistence.Database;
using Dapper;

namespace AnimeList.Persistence.Repositories;

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

    public async Task<Anime?> GetByIdAsync(int malId)
    {
        using var connection = _connectionFactory.CreateConnection();
        
        const string sql = @"SELECT *
                           FROM Anime
                           Where MalId = @MalId";

        return await connection.QuerySingleOrDefaultAsync<Anime>(sql, new { MalId = malId });
    }

    public async Task<IEnumerable<Anime>> GetByTitleAsync(string title)
    {
        using var connection = _connectionFactory.CreateConnection();
        
        const string sql = @"SELECT *
                           FROM Anime
                           WHERE Title LIKE @Pattern";
        
        return await connection.QueryAsync<Anime>(sql, new { Pattern = $"%{title}%" });
    }

    public async Task<IEnumerable<Anime>> GetByMinimumScoreAsync(double minScore)
    {
        using var connection = _connectionFactory.CreateConnection();
        
        const string sql = @"SELECT * 
                            FROM Anime 
                            WHERE score >= @MinScore";

        return await connection.QueryAsync<Anime>(sql, new { MinScore = minScore });
    }

    public async Task<IEnumerable<Anime>> GetByTypeAsync(AnimeEnums.AnimeType type)
    {
        using var connection = _connectionFactory.CreateConnection();

        const string sql = @"SELECT *
                           FROM Anime
                           WHERE type = @Type";
        
        return await connection.QueryAsync<Anime>(sql, new { Type = type });
    }

    public async Task<IEnumerable<Anime>> GetByStatusAsync(AnimeEnums.AnimeStatus status)
    {
        using var connection = _connectionFactory.CreateConnection();

        const string sql = @"SELECT *
                           FROM Anime
                           WHERE status = @Status";
        
        return await connection.QueryAsync<Anime>(sql, new { Status = status });
    }

    public async Task<IEnumerable<Anime>> GetByRatingAsync(AnimeEnums.AnimeRating rating)
    {
        using var connection = _connectionFactory.CreateConnection();

        const string sql = @"SELECT *
                           FROM Anime
                           WHERE rating = @Rating";
        
        return await connection.QueryAsync<Anime>(sql, new { Rating = rating });
    }
}