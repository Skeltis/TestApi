using Microsoft.EntityFrameworkCore;
using TestApp.Data.Contracts.Interfaces;
using TestApp.Data.Storage;

namespace TestApp.Data.Base;

public class UnitOfWork : IUnitOfWork
{
    private readonly MainDbContext _mainDbContext;
    private ICompaniesStorage? _companiesStorage;
    private IUsersStorage? _usersStorage;

    public ICompaniesStorage CompaniesStorage => _companiesStorage ??= new CompaniesStorage(_mainDbContext);
    public IUsersStorage UsersStorage => _usersStorage ??= new UsersStorage(_mainDbContext);

    public UnitOfWork(IDbContextFactory<MainDbContext> contextFactory)
    {
        _mainDbContext = contextFactory.CreateDbContext();
    }

    public void Dispose()
    {
        _mainDbContext.Dispose();
    }

    public ValueTask DisposeAsync() => _mainDbContext.DisposeAsync();

    public Task SaveChangesAsync()
    {
        return _mainDbContext.SaveChangesAsync();
    }
}
