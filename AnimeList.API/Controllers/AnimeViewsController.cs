using Microsoft.AspNetCore.Mvc;

namespace AnimeList.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimeViewsController : ControllerBase
{
    private readonly Application.Handlers.AnimeViews.AnimeViewQueryHandler _viewQueryHandler;
     
    public AnimeViewsController(Application.Handlers.AnimeViews.AnimeViewQueryHandler viewQueryHandler)
    {
        _viewQueryHandler = viewQueryHandler;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAnimeItem([FromRoute] int id) =>
        Ok(await _viewQueryHandler.GetAnimeItemViewAsync(id));

    [HttpGet("{id}/recommendations")]
    public async Task<IActionResult> GetAnimeRecommendationsView([FromRoute] int id) =>
        Ok(await _viewQueryHandler.GetAnimeRecommendationsViewAsync(id));

    [HttpGet("{id}/stats")]
    public async Task<IActionResult> GetAnimeStatsView([FromRoute] int id) =>
        Ok(await _viewQueryHandler.GetAnimeStatsViewAsync(id));
}
