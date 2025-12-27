using System.Data;
using Microsoft.Data.Sqlite;

namespace AnimeList.Persistence.Database;

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
    {
        var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using (var command = connection.CreateCommand())
        {
            command.CommandText = "PRAGMA foreign_keys = ON;";
            command.ExecuteNonQuery();
        }

        return connection;
    }
}