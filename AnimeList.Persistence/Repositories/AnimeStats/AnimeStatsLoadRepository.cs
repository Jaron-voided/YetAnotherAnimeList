using AnimeList.Application.RepoInterfaces.AnimeStats;
using AnimeList.Persistence.Database;
using Dapper;

namespace AnimeList.Persistence.Repositories.AnimeStats;

public class AnimeStatsLoadRepository : IAnimeStatsLoadRepository
{
    //This differs from AnimeLoadRepo, be sure to change one
    private readonly IDbConnectionFactory _connectionFactory;

    public AnimeStatsLoadRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    
    private const string insertSql = """
                                     INSERT INTO AnimeStats (
                                         MalId,
                                         Watching,
                                         Completed,
                                         OnHold,
                                         Dropped,
                                         PlanToWatch,
                                         Total,
                                         Score8Votes,
                                         Score9Votes,
                                         Score10Votes
                                     )
                                     VALUES (
                                         @MalId,
                                         @Watching,
                                         @Completed,
                                         @OnHold,
                                         @Dropped,
                                         @PlanToWatch,
                                         @Total,
                                         @Score8Votes,
                                         @Score9Votes,
                                         @Score10Votes
                                     );
                                     """;

    
    public async Task<bool> HasBeenLoadedAsync()
    {
        using var conn = _connectionFactory.CreateConnection();

        // this differs from AnimeLoadRepo, be sure to change one or the other
        string sql = "SELECT 1 FROM AnimeStats LIMIT 1";
        int count = await conn.ExecuteScalarAsync<int>(sql);

        return count > 0;
    }

    public async Task InsertAnimeStatsAsync(Domain.Models.AnimeStats animeStats)
    {
        using var conn = _connectionFactory.CreateConnection();
        
        await conn.ExecuteAsync(insertSql, animeStats);
    }

    public async Task InsertAllAnimeStatsAsync(IEnumerable<Domain.Models.AnimeStats> animeStats)
    {
        using var conn = _connectionFactory.CreateConnection();
        conn.Open();
        
        using var tx = conn.BeginTransaction();

        try
        {
            await conn.ExecuteAsync(
                insertSql,
                animeStats,
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