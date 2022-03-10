using TestApp.Data.Contracts.Models;

namespace TestApp.Data.Contracts.Interfaces;

public interface IUsersStorage
{
    Task<User> CreateAsync(User user, CancellationToken cancellationToken);
    Task DeleteByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<User?> GetAsync(Guid userId, CancellationToken cancellationToken);
    Task<User[]> GetUsersAsync(int take, int skip, CancellationToken cancellationToken);
    Task<User> UpdateAsync(User user, CancellationToken cancellationToken);
}
