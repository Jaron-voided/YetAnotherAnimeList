using AnimeList.Application.RepoInterfaces.AnimeRecommendations;
using AnimeList.Persistence.Database;
using Dapper;

namespace AnimeList.Persistence.Repositories.AnimeRecommendations;

public class AnimeRecommendationsLoadRepository : IAnimeRecommendationsLoadRepository
{
    private IDbConnectionFactory _dbConnectionFactory;

    public AnimeRecommendationsLoadRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    private const string insertSql = """
                                     INSERT INTO AnimeRecommendations (
                                         BaseMalId,
                                         SuggestedMalId,
                                         TimesSuggestedCount
                                     )
                                     VALUES (
                                         @BaseMalId,
                                         @SuggestedMalId,
                                         @TimesSuggestedCount
                                     );
                                     """;
    public async Task<bool> HasBeenLoadedAsync()
    {
        using var conn = _dbConnectionFactory.CreateConnection();

        string sql = "SELECT COUNT(1) FROM AnimeRecommendations LIMIT 1";
        int count = await conn.ExecuteScalarAsync<int>(sql);

        return count > 0;
    }

    public async Task InsertAllAnimeRecommendationsAsync(
        IEnumerable<Domain.Models.AnimeRecommendations> animeRecommendations)
    {
        Console.WriteLine(
            $"AnimeLoadRepo assumes it is being passed ${animeRecommendations.Count()} animesRecommendations");
        using var conn = _dbConnectionFactory.CreateConnection();
        conn.Open();

        using var tx = conn.BeginTransaction();

        try
        {
            await conn.ExecuteAsync(
                insertSql,
                animeRecommendations,
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