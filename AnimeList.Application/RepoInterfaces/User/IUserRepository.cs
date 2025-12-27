using AnimeList.Application.DTOs.User;
using AnimeList.Domain.Enums;
using AnimeList.Domain.Models;

namespace AnimeList.Application.RepoInterfaces.User;

public interface IUserRepository
{
    Task<Domain.Models.User?> GetUserByIdAsync(int userId);
    Task<Domain.Models.User?> GetUserByUsernameAsync(string username);
    //Task<IEnumerable<Domain.Models.UserAnimeEntry>> GetUserReadAnimeEntriesAsync(int userId);
    Task CreateUserAsync(Domain.Models.User user);
    Task DeleteUserAsync(int userId);
}