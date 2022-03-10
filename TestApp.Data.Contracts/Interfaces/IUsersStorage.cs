using TestApp.Data.Contracts.Models;

namespace TestApp.Data.Contracts.Interfaces;

public interface IUsersStorage
{
    Task<User> CreateAsync(User user, CancellationToken cancellationToken);
    Task<User?> GetAsync(Guid userId, CancellationToken cancellationToken);
    Task<User?> GetAsync(string email, CancellationToken cancellationToken);
}
