namespace TestApp.Data.Contracts.Interfaces;

public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    ICompaniesStorage CompaniesStorage { get; }
    IUsersStorage UsersStorage { get; }
    Task SaveChangesAsync();
}
