using TestApp.Data.Contracts.Models;

namespace TestApp.Data.Contracts.Interfaces;

public interface IUsersStorage
{
    Task<User> CreateAsync(User user, CancellationToken cancellationToken);
}
