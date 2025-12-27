using AnimeList.Application.RepoInterfaces.Anime;
using AnimeList.Persistence.Database;
using Dapper;

namespace AnimeList.Persistence.Repositories.Anime;

public class AnimeLoadRepository : IAnimeLoadRepository
{
    private IDbConnectionFactory _connectionFactory;

    public AnimeLoadRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    
    private const string insertSql = """
                                     INSERT INTO Anime (
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
    
    public async Task InsertAnimeAsync(Domain.Models.Anime anime)
    
    {
        using var conn = _connectionFactory.CreateConnection();
        
        await conn.ExecuteAsync(insertSql, anime);
    }

    // This method brings me down to...
    /*Parsing took 935 ms 
    , Mapping took 32 ms 
    , Loading DB took 1148 ms*/
    // Could look into batching, but this fast enough for now!!
    public async Task InsertAllAnimeAsync(IEnumerable<Domain.Models.Anime> animes)
    {
        Console.WriteLine($"AnimeLoadRepo assumes it is being passed ${animes.Count()} animes");
        using var conn = _connectionFactory.CreateConnection();
        conn.Open();
        
        using var tx = conn.BeginTransaction();

        try
        {
            await conn.ExecuteAsync(
                insertSql,
                animes,
                transaction: tx
            );

            tx.Commit();
        }
        catch
        {
            tx.Rollback();
            throw;
        }
    }    
    
    /* This method is 
    Parsing took 864 ms                                                                                                                                                                                                                 
    , Mapping took 30 ms 
    , Loading DB took 96681 ms*/
    /*public async Task InsertAllAnimeAsync(IEnumerable<Domain.Models.Anime> animes)
    {
        using var conn = _connectionFactory.CreateConnection();
        
        // This is the reason my program loads slow...
        await conn.ExecuteAsync(insertSql, animes);
    }*/
}