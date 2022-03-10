using TestApp.BLL.Contracts.Dtos;

namespace TestApp.BLL.Contracts.Interfaces;

public interface ICompaniesService
{
    Task<CompanyDto> AddCompanyAsync(CompanyDto company, CancellationToken cancellationToken);
    Task<CompanyDto> GetCompanyAsync(Guid companyId, CancellationToken cancellationToken);
}
