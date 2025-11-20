using AnimeProject.Domain.Models;

namespace AnimeProject.Application.Interfaces;

public interface IAnimeRepository
{
    Task<bool> HasBeenLoadedAsync();
    Task InsertAnimeAsync(Anime anime);
    //Task UpdateAnimeAsync(Anime anime);
    Task InsertAllAnimeAsync(IEnumerable<Anime> animes);
}