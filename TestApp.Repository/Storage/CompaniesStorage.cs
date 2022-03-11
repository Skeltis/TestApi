using Microsoft.EntityFrameworkCore;
using TestApp.Data.Contracts.Interfaces;
using TestApp.Data.Contracts.Models;

namespace TestApp.Data.Storage;

public class CompaniesStorage : ICompaniesStorage
{
    private readonly MainDbContext _dbContext;

    public CompaniesStorage(MainDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<Company> AddAsync(Company company, CancellationToken cancellationToken)
    {
        _dbContext.Set<Company>().Add(company);
        await _dbContext.Entry(company).Collection(m => m.Users).LoadAsync(cancellationToken);
        return company;
    }

    public Task<Company?> GetAsync(Guid companyId, CancellationToken cancellationToken)
    {
        return _dbContext.Set<Company>().Include(e => e.Users).FirstOrDefaultAsync(e => e.Id == companyId, cancellationToken);
    }

    public Task<Company?> GetAsync(string companyName, CancellationToken cancellationToken)
    {
        return _dbContext.Set<Company>().Include(e => e.Users).FirstOrDefaultAsync(e => e.CompanyName == companyName, cancellationToken);
    }
}

