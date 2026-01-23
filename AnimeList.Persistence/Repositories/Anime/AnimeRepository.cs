using System.Collections;
using AnimeList.Application.RepoInterfaces.Anime;
using AnimeList.Domain.Enums;
using AnimeList.Persistence.Database;
using Dapper;

namespace AnimeList.Persistence.Repositories.Anime;

public class AnimeRepository : IAnimeRepository
{
    private readonly IDbConnectionFactory _connectionFactory;
    
    public AnimeRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    public async Task<IEnumerable<Domain.Models.Anime>> GetAllAsync()
    {
        using var connection = _connectionFactory.CreateConnection();

        const string sql = "SELECT * FROM Anime";
        return await connection.QueryAsync<Domain.Models.Anime>(sql);
    }

    public async Task<HashSet<int>> GetAllMalIdsAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        
        const string sql = "SELECT MalId FROM Anime";

        var ids = await connection.QueryAsync<int>(sql);
        return ids.ToHashSet();
    }

    public async Task<Domain.Models.Anime?> GetByIdAsync(int malId)
    {
        using var connection = _connectionFactory.CreateConnection();
        
        const string sql = @"SELECT *
                           FROM Anime
                           Where MalId = @MalId";

        return await connection.QuerySingleOrDefaultAsync<Domain.Models.Anime>(sql, new { MalId = malId });
    }

    public async Task<IEnumerable<Domain.Models.Anime>> GetByTitleAsync(string title)
    {
        using var connection = _connectionFactory.CreateConnection();
        
        const string sql = @"SELECT *
                           FROM Anime
                           WHERE Title LIKE @Pattern";
        
        return await connection.QueryAsync<Domain.Models.Anime>(sql, new { Pattern = $"%{title}%" });
    }

    public async Task<IEnumerable<Domain.Models.Anime>> GetByMinimumScoreAsync(double minScore)
    {
        using var connection = _connectionFactory.CreateConnection();
        
        const string sql = @"SELECT * 
                            FROM Anime 
                            WHERE score >= @MinScore";

        return await connection.QueryAsync<Domain.Models.Anime>(sql, new { MinScore = minScore });
    }

    public async Task<IEnumerable<Domain.Models.Anime>> GetByTypeAsync(AnimeEnums.AnimeType type)
    {
        using var connection = _connectionFactory.CreateConnection();

        const string sql = @"SELECT *
                           FROM Anime
                           WHERE type = @Type";
        
        return await connection.QueryAsync<Domain.Models.Anime>(sql, new { Type = type });
    }

    public async Task<IEnumerable<Domain.Models.Anime>> GetByStatusAsync(AnimeEnums.AnimeStatus status)
    {
        using var connection = _connectionFactory.CreateConnection();

        const string sql = @"SELECT *
                           FROM Anime
                           WHERE status = @Status";
        
        return await connection.QueryAsync<Domain.Models.Anime>(sql, new { Status = status });
    }

    public async Task<IEnumerable<Domain.Models.Anime>> GetByRatingAsync(AnimeEnums.AnimeRating rating)
    {
        using var connection = _connectionFactory.CreateConnection();

        const string sql = @"SELECT *
                           FROM Anime
                           WHERE rating = @Rating";
        
        return await connection.QueryAsync<Domain.Models.Anime>(sql, new { Rating = rating });
    }

    public async Task<IEnumerable<Domain.Models.Anime>> GetMultipleAnimeByIdAsync(List<int> animeEntryIds)
    {
        using var connection = _connectionFactory.CreateConnection();

        const string sql = @"SELECT *
                         FROM Anime
                         WHERE MalId IN @Ids";

        return await connection.QueryAsync<Domain.Models.Anime>(sql, new { Ids = animeEntryIds });
    }

}