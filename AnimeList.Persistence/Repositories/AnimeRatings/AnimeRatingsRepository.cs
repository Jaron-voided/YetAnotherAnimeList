using AnimeList.Application.RepoInterfaces.AnimeRatings;
using AnimeList.Persistence.Database;
using Dapper;

namespace AnimeList.Persistence.Repositories.AnimeRatings;

public class AnimeRatingsRepository : IAnimeRatingsRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public AnimeRatingsRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    
    public async Task<IEnumerable<Domain.Models.AnimeRatings>> GetRatingsForUserAsync(string username, int limit)
    {
        using var connection = _connectionFactory.CreateConnection();

        const string sql = """
                           SELECT *
                           FROM  AnimeRatings
                           WHERE Username = @username
                           LIMIT @limit
                           """;

        return await connection.QueryAsync<Domain.Models.AnimeRatings>(sql, new
        {
            username,
            limit
        });
    }

    public async Task<IEnumerable<Domain.Models.AnimeRatings>> GetRatingsForAnimeAsync(int malId, int limit)
    {
        using var connection = _connectionFactory.CreateConnection();

        const string sql = """
                           SELECT *
                           FROM AnimeRatings
                           WHERE MalId = @malId
                           LIMIT @limit
                           """;

        return await connection.QueryAsync<Domain.Models.AnimeRatings>(sql, new
        {
            malId,
            limit
        });
    }
}