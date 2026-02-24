using System.Collections;

namespace AnimeList.Application.RepoInterfaces.AnimeStats;

public interface IAnimeStatsRepository
{
    Task<IEnumerable<Domain.Models.AnimeStats>> GetByMalIdAsync(int malId);

    Task<Domain.Models.AnimeStats> GetAnimeStatsByIdAsync(int malId);
}