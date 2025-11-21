using AnimeProject.Application.Handlers.Anime.Query;
using AnimeProject.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace AnimeList.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AnimeController : ControllerBase
{
    private readonly AnimeQueryHandler _queryHandler;

    public AnimeController(AnimeQueryHandler queryHandler)
    {
        _queryHandler = queryHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _queryHandler.GetAllAnimeAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) =>
        Ok(await _queryHandler.GetAnimeByIdAsync(id));

    [HttpGet("search")]
    public async Task<IActionResult> SearchByTitle([FromQuery] string title) =>
        Ok(await _queryHandler.GetAnimeByTitleAsync(title));

    [HttpGet("minScore")]
    public async Task<IActionResult> SearchByMinimumScore([FromQuery] double minScore) =>
        Ok(await _queryHandler.GetAnimeByMinimumScoreAsync(minScore));    
    
    [HttpGet("type")]
    public async Task<IActionResult> SearchByType([FromQuery] AnimeEnums.AnimeType type) =>
        Ok(await _queryHandler.GetAnimeByTypeAsync(type));    
    
    [HttpGet("status")]
    public async Task<IActionResult> SearchByStatus([FromQuery] AnimeEnums.AnimeStatus status) =>
        Ok(await _queryHandler.GetAnimeByStatusAsync(status));    
    
    [HttpGet("rating")]
    public async Task<IActionResult> SearchByRating([FromQuery] AnimeEnums.AnimeRating rating) =>
        Ok(await _queryHandler.GetAnimeByRatingAsync(rating));
      
    // Another for genres
    // More for combined controllers
}