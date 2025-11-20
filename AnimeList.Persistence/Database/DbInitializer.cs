using Dapper;

namespace AnimeProject.Persistence.Database;

public class DbInitializer
{
    private readonly IDbConnectionFactory _connectionFactory;

    public DbInitializer(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task InitializeDbAsync()
    {
        using var connection = _connectionFactory.CreateConnection();

        var sql = """
                  CREATE TABLE IF NOT EXISTS Anime (
                      MalId       INTEGER PRIMARY KEY,
                      Title       TEXT    NOT NULL,
                      ImageUrl    TEXT    NOT NULL,
                      Type        INTEGER NOT NULL,
                      Status      INTEGER NOT NULL,
                      Rating      INTEGER NOT NULL,
                      Score       REAL,
                      StartDate   TEXT,
                      EndDate     TEXT,
                      Synopsis    TEXT,
                      Rank        INTEGER,
                      Popularity  INTEGER,
                      Genres      TEXT,
                      Episodes    INTEGER,
                      Season      TEXT,
                      Year        INTEGER,
                      Streaming   TEXT
                  );
                  """;

        await connection.ExecuteAsync(sql);
    }
    
}