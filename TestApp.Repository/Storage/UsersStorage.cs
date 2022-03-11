using TestApp.Data.Contracts.Interfaces;
using TestApp.Data.Contracts.Models;

namespace TestApp.Data.Storage;

public class UsersStorage : IUsersStorage
{
    private readonly MainDbContext _dbContext;

    public UsersStorage(MainDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<User> CreateAsync(User user, CancellationToken cancellationToken)
    {
        _dbContext.Set<User>().Add(user);
        await _dbContext.Entry(user).Reference(x => x.Company).LoadAsync(cancellationToken);
        return user;
    }
}

