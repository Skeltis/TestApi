using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestApp.Data.Base;
using TestApp.Data.Contracts.Interfaces;
using TestApp.Data.Storage;

namespace TestApp.Data.DI;

public static class StorageDependencyInjection
{
    public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IUnitOfWorkFactory, UnitOfWorkFactory>();
        services.AddDbContextFactory<MainDbContext>(o =>
            o.UseNpgsql(configuration.GetConnectionString("IdentityServerDb")));

        services.AddSingleton<ICompaniesStorage, CompaniesStorage>();
        services.AddSingleton<IUsersStorage, UsersStorage>();

        return services;
    }
}

