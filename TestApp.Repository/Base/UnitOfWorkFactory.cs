using Microsoft.EntityFrameworkCore;
using TestApp.Data.Contracts.Interfaces;

namespace TestApp.Data.Base;

public class UnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly IDbContextFactory<MainDbContext> _mainDbContext;

    public UnitOfWorkFactory(IDbContextFactory<MainDbContext> mainDbContext)
    {
        _mainDbContext = mainDbContext ?? throw new ArgumentNullException(nameof(mainDbContext));
    }

    public IUnitOfWork Create()
    {
        return new UnitOfWork(_mainDbContext);
    }
}
