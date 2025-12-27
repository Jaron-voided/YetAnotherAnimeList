using AnimeList.Application.RepoInterfaces.AnimeRatings;
using AnimeList.Domain.Enums;
using AnimeList.Persistence.Database;
using Dapper;

namespace AnimeList.Persistence.Repositories.AnimeRatings;

public class AnimeRatingsLoadRepository : IAnimeRatingsLoadRepository
{
     private IDbConnectionFactory _connectionFactory;

    public AnimeRatingsLoadRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    
    private const string insertSql = """
                                     INSERT INTO AnimeRatings (
                                         Username,
                                         MalId,
                                         Status,
                                         Score,
                                         NumberOfWatchedEpisodes
                                     )
                                     VALUES (
                                         @Username,
                                         @MalId,
                                         @Status,
                                         @Score,
                                         @NumberOfWatchedEpisodes
                                     );
                                     """;

    public async Task<bool> HasBeenLoadedAsync()
    {
        using var conn = _connectionFactory.CreateConnection();

        string sql = "SELECT COUNT(1) FROM AnimeRatings LIMIT 1";
        int count = await conn.ExecuteScalarAsync<int>(sql);

        return count > 0;
    }

    public async Task InsertAnimeRatingAsync(Domain.Models.AnimeRatings animeRating)
    
    {
        using var conn = _connectionFactory.CreateConnection();
        
        await conn.ExecuteAsync(insertSql, animeRating);
    }
    
    // Could look into batching, but this fast enough for now!!
    public async Task InsertAllAnimeRatingsAsync(IEnumerable<Domain.Models.AnimeRatings> animeRatings)
    {
        var ratingsList = animeRatings.ToList();
        Console.WriteLine($"AnimeRatingsLoadRepo assumes it is being passed ${ratingsList.Count} animes");

        using var conn = _connectionFactory.CreateConnection();
        conn.Open();
        
        using var tx = conn.BeginTransaction();

        try
        {
            await conn.ExecuteAsync(
                insertSql,
                ratingsList,
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
   
}