using AnimeList.Application.DTOs.User;
using AnimeList.Application.Handlers.User;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AnimeList.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserQueryHandler _userQueryHandler;
    private readonly UserCommandHandler _userCommandHandler;

    public UsersController(UserQueryHandler userQueryHandler, UserCommandHandler userCommandHandler)
    {
        _userQueryHandler = userQueryHandler;
        _userCommandHandler = userCommandHandler;
    }
    
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserById(int userId) =>
        Ok(await _userQueryHandler.GetUserByIdAsync(userId));

    [HttpGet("by-username/{userName}")]
    public async Task<IActionResult> GetUserByUsername(string userName) =>
        Ok(await _userQueryHandler.GetUserByUsernameAsync(userName));

    [HttpPost]
    public async Task CreateUser([FromBody] CreateUserDto dto) =>
        await _userCommandHandler.CreateUserAsync(dto);

    [HttpDelete("{userId}")]
    public async Task DeleteUser(int userId) =>
        await _userCommandHandler.DeleteUserAsync(userId);

    [HttpGet("{userId}/entries")]
    public async Task<IActionResult> GetUserAnimeEntries([FromRoute] int userId) =>
        Ok(await _userQueryHandler.GetUserReadAnimeEntriesAsync(userId));
}