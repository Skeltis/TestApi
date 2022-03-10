using Microsoft.Extensions.DependencyInjection;
using TestApp.Api.Contracts.Interfaces.Mappers;
using TestApp.Api.Mappers;

namespace TestApp.Api.DI;

public static class ApiDependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddSingleton<IUserMapper, UserMapper>();
        services.AddSingleton<ICompanyMapper, CompanyMapper>();

        return services;
    }
}

