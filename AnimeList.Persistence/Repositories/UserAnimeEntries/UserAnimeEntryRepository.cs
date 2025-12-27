using AnimeList.Application.RepoInterfaces.UserAnimeEntry;
using AnimeList.Domain.Models;
using AnimeList.Persistence.Database;
using Dapper;

namespace AnimeList.Persistence.Repositories.UserAnimeEntries;

public class UserAnimeEntryRepository : IUserAnimeEntryRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public UserAnimeEntryRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    
    public async Task AddEntryAsync(UserAnimeEntry animeEntry)
    {
        using var connection = _connectionFactory.CreateConnection();
        
        const string sql = @"
            INSERT INTO UserAnimeEntries 
                (UserId, MalId, Rating, Comment, CurrentSeason, CurrentEpisode, WatchStatus, RewatchStatus)
            VALUES 
                (@UserId, @MalId, @Rating, @Comment, @CurrentSeason, @CurrentEpisode, @WatchStatus, @RewatchStatus)";

        await connection.ExecuteAsync(sql, animeEntry);
    }

    public async Task UpdateEntryAsync(UserAnimeEntry animeEntry)
    {
        using var connection = _connectionFactory.CreateConnection();

        const string sql = @"UPDATE UserAnimeEntries
                                SET 
                                    Rating = @Rating,
                                    Comment = @Comment,
                                    CurrentSeason = @CurrentSeason,
                                    CurrentEpisode = @CurrentEpisode,
                                    WatchStatus = @WatchStatus,
                                    RewatchStatus = @RewatchStatus
                                WHERE EntryId = @EntryId;";
        
        await connection.ExecuteAsync(sql, animeEntry);
    }

    public async Task DeleteEntryAsync(int entryId)
    {
        using var connection = _connectionFactory.CreateConnection();
        
        const string sql = @"DELETE FROM UserAnimeEntries
                             WHERE EntryId = @EntryId;";
        
        // Dapper replaces @EntryId with the value we pass in the anonymous object:
        //     new { EntryId = entryId }
        await connection.ExecuteAsync(sql, new {EntryId = entryId});
    }

    public async Task<UserAnimeEntry?> GetEntryAsync(int entryId)
    {
        using var connection = _connectionFactory.CreateConnection();

        const string sql = @"SELECT * 
                             FROM UserAnimeEntries 
                             WHERE EntryId = @EntryId";

        return await connection.QuerySingleOrDefaultAsync<UserAnimeEntry>(sql, new { EntryId = entryId });
    }

    /*
    public async Task<IEnumerable<UserAnimeEntry>> GetMultipleAnimeEntriesAsync(List<int> animeIds)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<UserAnimeEntry>> GetMultipleAnimeEntriesAsync(int userId, List<int> animeIds)
    {
        throw new NotImplementedException();
    }
    */

    public async Task<IEnumerable<UserAnimeEntry?>> GetAllEntriesOfUserAsync(int userId)
    {
        using var connection = _connectionFactory.CreateConnection();
        
        const string sql = @"SELECT * 
                             FROM UserAnimeEntries 
                             WHERE UserId = @UserId";
        
        return await connection.QueryAsync<UserAnimeEntry>(sql, new { UserId = userId });
    }
}