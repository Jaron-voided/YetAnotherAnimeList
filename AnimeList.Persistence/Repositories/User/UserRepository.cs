using AnimeList.Application.DTOs.User;
using AnimeList.Application.RepoInterfaces.User;
using AnimeList.Persistence.Database;
using Dapper;

namespace AnimeList.Persistence.Repositories.User;

public class UserRepository : IUserRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public UserRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _connectionFactory = dbConnectionFactory;
    }
    
    public async Task<Domain.Models.User?> GetUserByIdAsync(int userId)
    {
        using var connection = _connectionFactory.CreateConnection();
        
        const string sql = @"SELECT *
                           FROM Users
                           Where UserId = @UserId";

        return await connection.QuerySingleOrDefaultAsync<Domain.Models.User?>(sql, new { UserId = userId });
    }
    
    public async Task<Domain.Models.User?> GetUserByUsernameAsync(string userName)
    {
        using var connection = _connectionFactory.CreateConnection();
        
        const string sql = @"SELECT *
                           FROM Users
                           Where Username = @UserName";

        return await connection.QuerySingleOrDefaultAsync<Domain.Models.User?>(sql, new { UserName = userName });
    }

    public async Task CreateUserAsync(Domain.Models.User user)
    {
        using var connection = _connectionFactory.CreateConnection();
        
        const string sql = @"INSERT INTO Users (Username, Email)
                                VALUES (@Username, @Email)";
        
        await connection.ExecuteAsync(sql, user);
    }

    public async Task DeleteUserAsync(int userId)
    {
        using var connection = _connectionFactory.CreateConnection();
        
        const string sql = @"DELETE FROM Users 
                             WHERE UserId = @UserId";
        
        // Dapper replaces @UserId with the value we pass in the anonymous object:
        //     new { UserId = userId }
        await connection.ExecuteAsync(sql, new { UserId = userId });
    }
}