using Microsoft.Extensions.Logging;
using TestApp.BLL.Contracts.Dtos;
using TestApp.BLL.Contracts.Exceptions;
using TestApp.BLL.Contracts.Interfaces;
using TestApp.BLL.Contracts.Mappers;
using TestApp.Data.Contracts.Interfaces;

namespace TestApp.BLL.Services;

public class CompaniesService : BaseService<CompaniesService>, ICompaniesService
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly ICompanyMapper _companyMapper;

    public CompaniesService(IUnitOfWorkFactory unitOfWorkFactory, ICompanyMapper companyMapper,
        ILogger<CompaniesService> logger)
        : base(logger)
    {
        _unitOfWorkFactory = unitOfWorkFactory ?? throw new ArgumentNullException(nameof(unitOfWorkFactory));
        _companyMapper = companyMapper ?? throw new ArgumentNullException(nameof(companyMapper));
    }

    public async Task<CompanyDto> GetCompanyAsync(Guid companyId, CancellationToken cancellationToken)
    {
        using var uow = _unitOfWorkFactory.Create();
        {
            var company = await uow.CompaniesStorage.GetAsync(companyId, cancellationToken);
            if (company == null)
            {
                Logger.LogError($"Company with id={companyId} doesn't exist");
                throw new NotFoundException($"Company with id={companyId} doesn't exist");
            }
            return _companyMapper.Map(company);
        }
    }

    public async Task<CompanyDto> AddCompanyAsync(CompanyDto company, CancellationToken cancellationToken)
    {
        using var uow = _unitOfWorkFactory.Create();
        {
            var companyEntity = _companyMapper.Map(company);
            var addedCompany = await uow.CompaniesStorage.AddAsync(companyEntity, cancellationToken);
            await uow.SaveChangesAsync();
            return _companyMapper.Map(addedCompany);
        }
    }
}

