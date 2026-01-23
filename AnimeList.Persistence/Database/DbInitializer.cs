using Dapper;

namespace AnimeList.Persistence.Database;

public class DbInitializer
{
    private readonly IDbConnectionFactory _connectionFactory;

    public DbInitializer(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task InitializeDbAsync()
    {
        await InitializeAnimeTableAsync();
        await InitializeUserTableAsync();
        await InitializeUserAnimeEntryTableAsync();
        await InitializeAnimeStatsTableAsync();
        await InitializeAnimeRecommendationsTableAsync();
        await InitializeAnimeRatingsTableAsync();
    }

    private async Task InitializeAnimeTableAsync()
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
                      Year        INTEGER,
                      Streaming   TEXT
                  );
                  """;
        await connection.ExecuteAsync(sql);
    }

    private async Task InitializeUserTableAsync()
    {
        using var connection = _connectionFactory.CreateConnection();

        var sql = """
                  CREATE TABLE IF NOT EXISTS Users (
                      UserId    INTEGER  PRIMARY KEY AUTOINCREMENT,
                      Username  TEXT     NOT NULL,
                      Email     TEXT     NOT NULL
                  );
                  """;
        await connection.ExecuteAsync(sql);
    }  
    
    private async Task InitializeUserAnimeEntryTableAsync()
    {
        using var connection = _connectionFactory.CreateConnection();

        var sql = """
                  CREATE TABLE IF NOT EXISTS UserAnimeEntries (
                      EntryId           INTEGER PRIMARY KEY AUTOINCREMENT,
                      UserId            INTEGER NOT NULL,
                      MalId             INTEGER NOT NULL,
                      Rating            REAL,
                      Comment           TEXT,
                      CurrentSeason     INTEGER,
                      CurrentEpisode    INTEGER,
                      WatchStatus       INTEGER NOT NULL,
                      RewatchStatus     INTEGER,
                                                              
                      FOREIGN KEY (UserId) REFERENCES Users (UserId),
                      FOREIGN KEY(MalId) REFERENCES Anime (MalId)
                  
                  );
                  """;
        
        await connection.ExecuteAsync(sql);
    }
    
    private async Task InitializeAnimeStatsTableAsync()
    {
        using var connection = _connectionFactory.CreateConnection();

        var sql = """
                  CREATE TABLE IF NOT EXISTS AnimeStats (
                      MalId           INTEGER PRIMARY KEY,
                      Watching        INTEGER NOT NULL,
                      Completed       INTEGER NOT NULL,
                      OnHold          INTEGER NOT NULL,
                      Dropped         INTEGER NOT NULL,
                      PlanToWatch     INTEGER NOT NULL,
                      Total           INTEGER NOT NULL,
                      Score8Votes     REAL,
                      Score9Votes     REAL,
                      Score10Votes    REAL,
                      FOREIGN KEY (MalId) REFERENCES Anime(MalId)
                  );
                  """;

        await connection.ExecuteAsync(sql);
    }

    private async Task InitializeAnimeRecommendationsTableAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        
        var sql = """
                  CREATE TABLE IF NOT EXISTS AnimeRecommendations (
                      BaseMalId             INTEGER NOT NULL,
                      SuggestedMalId        INTEGER NOT NULL,
                      TimesSuggestedCount   INTEGER NOT NULL,
                      
                      PRIMARY KEY (BaseMalId, SuggestedMalId),
       
                      FOREIGN KEY (BaseMalId) REFERENCES Anime (MalId),
                      FOREIGN KEY (SuggestedMalId) REFERENCES Anime (MalId)
                  );
                  """;

        await connection.ExecuteAsync(sql);
    }    
    
    private async Task InitializeAnimeRatingsTableAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        
        var sql = """
                  CREATE TABLE IF NOT EXISTS AnimeRatings (
                      Username                  TEXT    NOT NULL,
                      MalId                     INTEGER NOT NULL,
                      Status                    INTEGER NOT NULL,
                      Score                     INTEGER NOT NULL,
                      NumberOfWatchedEpisodes   INTEGER NOT NULL,
                      
                      PRIMARY KEY (Username, MalId),
                      
                      FOREIGN KEY (MalId) REFERENCES Anime (MalId)
                  );
                  """;
        
        await connection.ExecuteAsync(sql);
    }
}