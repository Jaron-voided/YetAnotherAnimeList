using AnimeList.Application.RepoInterfaces.AnimeRatings;
using AnimeList.Application.TasteRecommendations;
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

    public async Task<IReadOnlyList<string>> GetSimilarUsersAsync(IEnumerable<int> malIds, int minScore, int minCommonAnime)
    {
        // Select username, from AnimeRatings table, where MalId in malIds and Score >= minScore, group by username where count is >= minCommonAnime
        using var connection = _connectionFactory.CreateConnection();
        const string sql = """
                           SELECT Username
                           FROM AnimeRatings
                           WHERE MalId IN @malIds AND Score >= @minScore
                           GROUP BY Username
                           HAVING COUNT(DISTINCT MalId) >= @minCommonAnime
                           ORDER BY COUNT(DISTINCT MalId) DESC
                           LIMIT 30000
                           """;

        var users = await connection.QueryAsync<string>(sql, new
        {
            malIds,
            minScore,
            minCommonAnime
        });

        return users.ToList();
    }

    public async Task<IReadOnlyList<RecommendedAnime>> GetRecommendedAnimesAsync(IEnumerable<string> users, IEnumerable<int> seedIds, int minScore, int maxRecommendations)
    {
        // From my list of users, get all their anime rated >= minScore that are not contained in my seedIds, group by MalId, order by count of ratings desc, limit maxRecommendations
        // Return an int for how many times each anime was rated by similar users, along with the anime ID MalId
        using var connection = _connectionFactory.CreateConnection();
        const string sql = """
                           SELECT MalId, COUNT(DISTINCT Username) AS RecommendationCount
                           FROM AnimeRatings
                           WHERE Username IN @users AND Score >= @minScore AND MalId NOT IN @seedIds
                           GROUP BY MalId
                           ORDER BY RecommendationCount DESC
                           LIMIT @maxRecommendations
                           """;

        var recommendations = await connection.QueryAsync<RecommendedAnime>(sql, new
        {
            users,
            minScore,
            seedIds,
            maxRecommendations
        });

        return recommendations.ToList();
    }
}