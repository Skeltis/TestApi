using TestApp.Data.Contracts.Models;

namespace TestApp.Data.Contracts.Interfaces;

public interface ICompaniesStorage
{
    Task<Company> AddAsync(Company company, CancellationToken cancellationToken);
    Task DeleteByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken);
    Task<Company?> GetAsync(Guid companyId, CancellationToken cancellationToken);
    Task<Company> UpdateAsync(Company company, CancellationToken cancellationToken);
}
