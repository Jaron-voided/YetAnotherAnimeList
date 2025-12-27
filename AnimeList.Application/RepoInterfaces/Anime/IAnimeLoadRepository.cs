namespace AnimeList.Application.RepoInterfaces.Anime;

public interface IAnimeLoadRepository
{
    Task<bool> HasBeenLoadedAsync();
    Task InsertAnimeAsync(Domain.Models.Anime anime);
    Task InsertAllAnimeAsync(IEnumerable<Domain.Models.Anime> animes);
}