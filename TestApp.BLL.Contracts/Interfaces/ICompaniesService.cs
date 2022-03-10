using TestApp.BLL.Contracts.Dtos;

namespace TestApp.BLL.Contracts.Interfaces;

public interface ICompaniesService
{
    Task<CompanyDto> AddCompanyWithUserAsync(CompanyDto company, UserDto user, 
        CancellationToken cancellationToken);
    Task<CompanyDto> AddCompanyAsync(CompanyDto company, CancellationToken cancellationToken);
}
