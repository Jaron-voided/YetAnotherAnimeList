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
        // Parse the connection string so we can safely extract the DB file path
        var builder = new SqliteConnectionStringBuilder(_connectionString);
        var dbPath = builder.DataSource;

        // Ensure the directory for the database file exists
        var directory = Path.GetDirectoryName(dbPath);
        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Now it's safe to open the connection
        var connection = new SqliteConnection(_connectionString);
        connection.Open();

        // Enable foreign keys (SQLite defaults this OFF)
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "PRAGMA foreign_keys = ON;";
            command.ExecuteNonQuery();
        }

        return connection;
    }
    
    /*public IDbConnection CreateConnection()
    {
        var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using (var command = connection.CreateCommand())
        {
            command.CommandText = "PRAGMA foreign_keys = ON;";
            command.ExecuteNonQuery();
        }

        return connection;
    }*/
}