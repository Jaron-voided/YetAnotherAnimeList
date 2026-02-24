using Microsoft.Data.Sqlite;

namespace AnimeList.Tests.Integration;

public static class DbTestSchema
{
    public static void CreateAnimeTable(SqliteConnection connection)
    {
        const string sql = """
        CREATE TABLE IF NOT EXISTS Anime (
            MalId INTEGER PRIMARY KEY,
            Title TEXT NOT NULL,
            ImageUrl TEXT,
            Type INTEGER,
            Status INTEGER,
            Rating INTEGER,
            Score REAL,
            StartDate TEXT,
            EndDate TEXT,
            Synopsis TEXT,
            Rank INTEGER,
            Popularity INTEGER,
            Genres TEXT,
            Episodes INTEGER,
            Year INTEGER,
            Streaming TEXT
        );
        """;

        using var cmd = connection.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }
}
