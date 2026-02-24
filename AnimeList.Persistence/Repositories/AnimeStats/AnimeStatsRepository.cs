using AnimeList.Application.RepoInterfaces.AnimeStats;
using AnimeList.Persistence.Database;
using Dapper;

namespace AnimeList.Persistence.Repositories.AnimeStats;

public class AnimeStatsRepository : IAnimeStatsRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public AnimeStatsRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Domain.Models.AnimeStats?> GetAnimeStatsByIdAsync(int malId)
    {
        using var connection = _connectionFactory.CreateConnection();

        const string sql = "SELECT * FROM AnimeStats WHERE MalId = @MalId";

        return await connection.QuerySingleOrDefaultAsync<Domain.Models.AnimeStats>(sql, new { MalId = malId });
    }

    public Task<IEnumerable<Domain.Models.AnimeStats>> GetByMalIdAsync(int malId)
    {
        throw new NotImplementedException();
    }
}