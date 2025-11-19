using Moondesk.Domain.Models;

namespace Moondesk.Domain.Interfaces.Services;

public interface IUserService
{
    Task<User?> GetUserAsync(string id);
    Task<User> CreateUserAsync(string id, string username, string email, string firstName, string lastName);
    Task<User> UpdateUserAsync(User user);
    Task DeleteUserAsync(string id);
    Task<bool> IsUsernameAvailableAsync(string username);
    Task<bool> IsEmailAvailableAsync(string email);
    Task CompleteOnboardingAsync(string userId);
}
