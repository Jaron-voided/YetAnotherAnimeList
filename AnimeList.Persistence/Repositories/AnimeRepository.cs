using AnimeProject.Application.Interfaces;
using AnimeProject.Domain.Models;
using AnimeProject.Persistence.Database;
using Dapper;

namespace AnimeProject.Persistence.Repositories;

public class AnimeRepository : IAnimeRepository
{
    private IDbConnectionFactory _connectionFactory;

    public AnimeRepository(IDbConnectionFactory connectionFactory)
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
                                         Season,
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
                                         @Season,
                                         @Year,
                                         @Streaming
                                     );
                                     """;

    public async Task<bool> HasBeenLoadedAsync()
    {
        using var conn = _connectionFactory.CreateConnection();

        string sql = "SELECT COUNT(1) FROM Anime LIMIT 1";
        int count = await conn.ExecuteScalarAsync<int>(sql);
        
        /*if (count == 0) return false;
        return true;*/
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