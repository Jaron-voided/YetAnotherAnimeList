using AnimeProject.Application.Handlers.DTOs;
using AnimeProject.Domain.Models;

namespace AnimeProject.Application.Mapping;

public class AnimeMapper : IAnimeMapper
{
    public AnimeDto ToDto(Anime anime)
    {
        return new AnimeDto
        {
            MalId = anime.MalId,
            Title = anime.Title,
            ImageUrl = anime.ImageUrl,
            Type = anime.Type.ToString(),
            Status = anime.Status.ToString(),
            Rating = anime.Rating.ToString(),
            Score = anime.Score,
            StartDate = anime.StartDate,
            EndDate = anime.EndDate,
            Synopsis = anime.Synopsis,
            Rank = anime.Rank,
            Popularity = anime.Popularity,
            Genres = anime.Genres,
            Episodes = anime.Episodes,
            Season = anime.Season,
            Year = anime.Year ?? anime.StartDate?.Year,
            Streaming = anime.Streaming
        };
    }

    public IEnumerable<AnimeDto> ToDtoList(IEnumerable<Anime> animes)
    {
        return animes.Select(ToDto);
    }
}