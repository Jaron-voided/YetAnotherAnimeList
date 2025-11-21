using AnimeProject.Domain.Models;

namespace AnimeProject.Application.Interfaces;

public interface IAnimeLoadRepository
{
    Task<bool> HasBeenLoadedAsync();
    Task InsertAnimeAsync(Anime anime);
    Task InsertAllAnimeAsync(IEnumerable<Anime> animes);
}