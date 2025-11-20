using System.Data;
using Microsoft.Data.Sqlite;

namespace AnimeProject.Persistence.Database;

public class SqliteConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public SqliteConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    // SqliteConnection implements IDbConnection, so it can promise IDbConnection and return SqliteConnection
    // This is a form of polymorphism
    public IDbConnection CreateConnection() 
        => new SqliteConnection(_connectionString);
}