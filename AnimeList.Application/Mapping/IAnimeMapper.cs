using AnimeProject.Application.Handlers.DTOs;
using AnimeProject.Domain.Models;

namespace AnimeProject.Application.Mapping;

public interface IAnimeMapper
{
    AnimeDto ToDto(Anime anime);
    
    IEnumerable<AnimeDto> ToDtoList(IEnumerable<Anime> animes);
}