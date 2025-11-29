using AnimeList.Application.Interfaces;
using AnimeList.Domain.Models;
using AnimeList.Persistence.Database;
using Dapper;

namespace AnimeList.Persistence.Repositories;

public class AnimeLoadRepository : IAnimeLoadRepository
{
    private IDbConnectionFactory _connectionFactory;

    public AnimeLoadRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    
    private const string insertSql = """
                                     INSERT OR IGNORE INTO Anime (
                                         MalId,
                                         Title,
                                         ImageUrl,
                                         Type,
                                         Status,
                                         Rating,
                                         Score,
                                         StartDate,
                                         EndDate,
                                         Synopsis,
                                         Rank,
                                         Popularity,
                                         Genres,
                                         Episodes,
                                         Year,
                                         Streaming
                                     )
                                     VALUES (
                                         @MalId,
                                         @Title,
                                         @ImageUrl,
                                         @Type,
                                         @Status,
                                         @Rating,
                                         @Score,
                                         @StartDate,
                                         @EndDate,
                                         @Synopsis,
                                         @Rank,
                                         @Popularity,
                                         @Genres,
                                         @Episodes,
                                         @Year,
                                         @Streaming
                                     );
                                     """;

    public async Task<bool> HasBeenLoadedAsync()
    {
        using var conn = _connectionFactory.CreateConnection();

        string sql = "SELECT COUNT(1) FROM Anime LIMIT 1";
        int count = await conn.ExecuteScalarAsync<int>(sql);

        return count > 0;
    }
    
    public async Task InsertAnimeAsync(Anime anime)
    {
        using var conn = _connectionFactory.CreateConnection();
        
        await conn.ExecuteAsync(insertSql, anime);
    }

    public async Task InsertAllAnimeAsync(IEnumerable<Anime> animes)
    {
        using var conn = _connectionFactory.CreateConnection();

        await conn.ExecuteAsync(insertSql, animes);
    }
}