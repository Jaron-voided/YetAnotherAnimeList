using AnimeList.Application.Handlers.TasteRecommendations;
using AnimeList.Application.TasteRecommendations;
using Microsoft.AspNetCore.Mvc;

namespace AnimeList.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class TasteRecommendationsController : ControllerBase
{
    private readonly TasteQueryHandler _tasteQueryHandler;

    public TasteRecommendationsController(TasteQueryHandler tasteQueryHandler)
    {
        _tasteQueryHandler = tasteQueryHandler;
    }

    [HttpPost]
    public async Task<IActionResult> GenerateRecommendations([FromBody] TasteRecommendationRequestDto request) =>
        Ok(await _tasteQueryHandler.GetTasteRecommendationsAsync(request));
}
