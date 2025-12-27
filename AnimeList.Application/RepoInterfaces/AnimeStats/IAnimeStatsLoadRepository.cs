namespace AnimeList.Application.RepoInterfaces.AnimeStats;

public interface IAnimeStatsLoadRepository
{
    Task<bool> HasBeenLoadedAsync();
    Task InsertAnimeStatsAsync(Domain.Models.AnimeStats animeStats);
    Task InsertAllAnimeStatsAsync(IEnumerable<Domain.Models.AnimeStats> animeStats);
}