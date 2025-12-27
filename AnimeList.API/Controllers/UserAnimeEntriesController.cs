/*using AnimeList.Application.DTOs.Anime;
using Microsoft.AspNetCore.Mvc;

namespace AnimeList.API.Controllers;


[ApiController]
[Route("api/users/{userId}/anime-entries")]
public class UserAnimeEntriesController : ControllerBase
{
    private readonly AnimeEntryQueryHandler _queryHandler;
    private readonly AnimeEntryCommandHandler _commandHandler;

    public UserAnimeEntriesController(
        AnimeEntryQueryHandler queryHandler,
        AnimeEntryCommandHandler commandHandler)
    {
        _queryHandler = queryHandler;
        _commandHandler = commandHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromRoute] int userId) =>
        Ok(await _queryHandler.GetAllAnimeEntriesAsync(userId));

    [HttpGet("{malId}")]
    public async Task<IActionResult> GetById(
        [FromRoute] int userId,
        [FromRoute] int malId) =>
        Ok(await _queryHandler.GetAnimeEntryById(userId, malId));

    [HttpPost]
    public async Task<IActionResult> PostAnimeEntry(
        [FromRoute] int userId, 
        [FromBody]AnimeDto anime) =>
        Ok(await _commandHandler.PostAnimeEntryAsync(userId, anime));

    [HttpPut("{malId}")]
    public async Task<IActionResult> EditAnimeEntry(
        [FromRoute] int userId, 
        [FromRoute] int malId, 
        [FromBody] AnimeDto anime) =>
        Ok(await _commandHandler.EditAnimeEntryAsync(userId, anime, malId));
    
    [HttpDelete("{malId}")]
    public async Task<IActionResult> DeleteAnimeEntry(
        [FromRoute] int userId, 
        [FromRoute] int malId) =>
        Ok(await _commandHandler.DeleteAnimeEntryAsync(userId, malId));
}*/