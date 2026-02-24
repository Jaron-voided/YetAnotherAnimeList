using AnimeList.Persistence.Repositories.Anime;
using AnimeList.Tests.TestData;
using Microsoft.Data.Sqlite;
using System;
using Xunit;

namespace AnimeList.Tests.Integration;

public class AnimeRepoTests 
    : IAsyncLifetime, IDisposable
{
    private const string ConnectionString = "Data Source=TestDatabase.db;Pooling=False";

    private readonly SqliteConnection _connection;
    private readonly AnimeLoadRepository _loadRepo;
    private readonly AnimeRepository _queryRepo;
    
    public AnimeRepoTests()
    {
        if (File.Exists("TestDatabase.db"))
            File.Delete("TestDatabase.db");
        _connection = new SqliteConnection(ConnectionString);
        _connection.Open();

        DbTestSchema.CreateAnimeTable(_connection);

        var factory = new TestConnectionFactory(ConnectionString);
        _loadRepo = new AnimeLoadRepository(factory);
        _queryRepo = new AnimeRepository(factory);
    }
    
    // Runs before each [Fact]
    public async Task InitializeAsync()
    {
        await _loadRepo.InsertAllAnimeAsync(AnimeSeedData.FiveAnimes());
    }

    // Runs after each [Fact]
    public Task DisposeAsync() => Task.CompletedTask;

    // Teardown
    public void Dispose() {
        _connection.Dispose(); 
        if (File.Exists("TestDatabase.db"))
        {
            File.Delete("TestDatabase.db");
            if (File.Exists("TestDatabase.db"))
                throw new Exception("DB file was not deleted (still exists)");

        }
    }

    [Fact]
    public async Task GetAll_ReturnsFiveAnime()
    {
        var loaded = await _loadRepo.HasBeenLoadedAsync();
        
        var all = await _queryRepo.GetAllAsync();
        var count = all.Count();

        Assert.True(loaded);
        Assert.Equal(5, count);
    }

    [Fact]
    public async Task InsertAllAnimeAsync_QueryById()
    {
        var fmab = await _queryRepo.GetByIdAsync(5114);

        Assert.NotNull(fmab);
        Assert.Equal("Fullmetal Alchemist: Brotherhood", fmab.Title);
    }

    [Fact]
    public async Task InsertAllAnimeAsync_QueryByTitle()
    {
        var fmab = (await _queryRepo.GetByTitleAsync("Fullmetal Alchemist: Brotherhood")).First();

        Assert.NotNull(fmab);
        Assert.Equal(5114, fmab.MalId);
    }
}
