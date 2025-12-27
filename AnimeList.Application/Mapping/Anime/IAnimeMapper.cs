using AnimeList.Application.DTOs.Anime;

namespace AnimeList.Application.Mapping.Anime;

public interface IAnimeMapper
{
    AnimeDto ToDto(Domain.Models.Anime anime);
    
    IEnumerable<AnimeDto> ToDtoList(IEnumerable<Domain.Models.Anime> animes);
}