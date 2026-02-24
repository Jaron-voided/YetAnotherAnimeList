using AnimeList.Application.RepoInterfaces.AnimeRecommendations;
using AnimeList.Domain.Projections;
using AnimeList.Persistence.Database;
using Dapper;

namespace AnimeList.Persistence.Repositories.AnimeRecommendations;

public class AnimeRecommendationsRepository : IAnimeRecommendationsRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public AnimeRecommendationsRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    
    public async Task<IEnumerable<Domain.Models.AnimeRecommendations>> GetAllAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        
        const string sql = "SELECT * FROM AnimeRecommendations;";
        return await connection.QueryAsync<Domain.Models.AnimeRecommendations>(sql);
    }

    public async Task<bool> ExistsAsync(int baseMalId, int suggestedMalId)
    {
        using var connection =  _connectionFactory.CreateConnection();

        const string sql = """
                           SELECT EXISTS (
                                SELECT 1
                                FROM AnimeRecommendations
                                WHERE BaseMalId = @baseMalId
                                AND SuggestedMalId = @suggestedMalId
                                );
                           """;

        // Scalar is one value, a bool string int ec
        return await connection.ExecuteScalarAsync<bool>(sql, new
        {
            baseMalId,
            suggestedMalId
        });
    }

    public async Task<IEnumerable<AnimeRecommendationProjections.RecommendationEdge>> GetRecommendationsForBaseAnimeAsync(int baseMalId, int limit)
    {
        using var connection =  _connectionFactory.CreateConnection();
        
        // The projection renames TimesSuggestedCount to TimesSuggested...
        const string sql = """
                           SELECT
                               BaseMalId,
                               SuggestedMalId,
                               TimesSuggestedCount AS TimesSuggested
                           FROM AnimeRecommendations
                           WHERE BaseMalId = @baseMalId
                           ORDER BY TimesSuggestedCount DESC
                           LIMIT @limit
                           """;

        return await connection.QueryAsync<AnimeRecommendationProjections.RecommendationEdge>(sql, new
        {
            baseMalId,
            limit
        });
    }

    public async Task<IEnumerable<AnimeRecommendationProjections.RecommendationEdge>> GetRecommendationsForMultipleBaseAnimeAsync(IEnumerable<int> baseMalIds, int perBaseLimit)
    {
        using var connection = _connectionFactory.CreateConnection();

        const string sql = """
                           SELECT
                                BaseMalId,
                                SuggestedMalId,
                                TimesSuggestedCount AS TimesSuggested
                           FROM AnimeRecommendations
                           WHERE BaseMalId IN @baseMalIds
                           ORDER BY TimesSuggestedCount DESC
                           LIMIT @perBaseLimit
                           """;
        
        return await connection.QueryAsync<AnimeRecommendationProjections.RecommendationEdge>(sql, new
        {
            baseMalIds,
            perBaseLimit
        });
    }

    public async Task<IEnumerable<AnimeRecommendationProjections.RecommendationEdge>> GetBasesForSuggestedAnime(int suggestedMalId)
    {
        using var connection = _connectionFactory.CreateConnection();

        const string sql = """
                           SELECT
                                BaseMalId,
                                SuggestedMalId,
                                TimesSuggestedCount AS TimesSuggested
                           FROM AnimeRecommendations
                           WHERE SuggestedMalId = @suggestedMalId
                           ORDER BY BaseMalId, TimesSuggestedCount DESC
                           """;

        return await connection.QueryAsync<AnimeRecommendationProjections.RecommendationEdge>(sql, new
        {
            suggestedMalId
        });
    }

    public async Task<List<int>> GetAllSuggestedIdsForBaseAsync(int baseMalId)
    {
        using var connection = _connectionFactory.CreateConnection();

        const string sql = """
                           SELECT SuggestedMalId
                           FROM AnimeRecommendations
                           WHERE BaseMalId = @baseMalId
                           """;

        return (await connection.QueryAsync<int>(sql, new { baseMalId}))
            .Distinct()
            .ToList();
    }
}