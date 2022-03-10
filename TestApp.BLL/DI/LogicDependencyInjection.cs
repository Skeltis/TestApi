using Microsoft.Extensions.DependencyInjection;
using TestApp.BLL.Contracts.Interfaces;
using TestApp.BLL.Contracts.Mappers;
using TestApp.BLL.Mappers;
using TestApp.BLL.Services;

namespace TestApp.BLL.DI;

public static class LogicDependencyInjection
{
    public static IServiceCollection AddLogic(this IServiceCollection services)
    {
        services.AddSingleton<ICompaniesService, CompaniesService>();
        services.AddSingleton<IUsersService, UsersService>();

        services.AddSingleton<ICompanyMapper, CompanyMapper>();
        services.AddSingleton<IUserMapper, UserMapper>();

        return services;
    }
}

