using Microsoft.EntityFrameworkCore;
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

    public Task<User?> GetAsync(Guid userId, CancellationToken cancellationToken)
    {
        return _dbContext.Set<User>().Include(e => e.Company).FirstOrDefaultAsync(e => e.Id == userId, cancellationToken);
    }

    public Task<User?> GetAsync(string email, CancellationToken cancellationToken)
    {
        return _dbContext.Set<User>().Include(e => e.Company).FirstOrDefaultAsync(e => e.Email == email, cancellationToken);
    }

    public async Task<User> CreateAsync(User user, CancellationToken cancellationToken)
    {
        _dbContext.Set<User>().Add(user);
        await _dbContext.Entry(user).Reference(x => x.Company).LoadAsync(cancellationToken);
        return user;
    }
}

