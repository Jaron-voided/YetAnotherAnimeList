using AnimeList.Application.Handlers.DTOs;
using AnimeList.Domain.Models;

namespace AnimeList.Application.Mapping;

public interface IAnimeMapper
{
    AnimeDto ToDto(Anime anime);
    
    IEnumerable<AnimeDto> ToDtoList(IEnumerable<Anime> animes);
}