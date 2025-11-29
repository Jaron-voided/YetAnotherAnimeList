using AnimeList.Application.Handlers.DTOs;
using AnimeList.Domain.Models;

namespace AnimeList.Application.Mapping;

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
            StartDate = MapDate(anime.StartDate),
            EndDate = MapDate(anime.EndDate),
            Synopsis = anime.Synopsis,
            Rank = anime.Rank,
            Popularity = anime.Popularity,
            Genres = anime.Genres,
            Episodes = anime.Episodes,
            Year = anime.Year ?? anime.StartDate?.Year,
            Streaming = anime.Streaming
        };
    }
    
    private DateOnly? MapDate(DateTime? dateTime)
    {
        if (dateTime != null)
            return DateOnly.FromDateTime(dateTime.Value);
        return null;
    }

    public IEnumerable<AnimeDto> ToDtoList(IEnumerable<Anime> animes)
    {
        return animes.Select(ToDto);
    }
}