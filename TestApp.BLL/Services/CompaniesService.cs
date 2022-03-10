using Microsoft.Extensions.Logging;
using TestApp.BLL.Contracts.Dtos;
using TestApp.BLL.Contracts.Exceptions;
using TestApp.BLL.Contracts.Interfaces;
using TestApp.BLL.Contracts.Mappers;
using TestApp.Data.Contracts.Interfaces;
using TestApp.Data.Contracts.Models;

namespace TestApp.BLL.Services;

public class CompaniesService : BaseService<CompaniesService>, ICompaniesService
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly ICompanyMapper _companyMapper;
    private readonly IUserMapper _userMapper;

    public CompaniesService(IUnitOfWorkFactory unitOfWorkFactory, ICompanyMapper companyMapper,
        IUserMapper userMapper, ILogger<CompaniesService> logger)
        : base(logger)
    {
        _unitOfWorkFactory = unitOfWorkFactory ?? throw new ArgumentNullException(nameof(unitOfWorkFactory));
        _companyMapper = companyMapper ?? throw new ArgumentNullException(nameof(companyMapper));
        _userMapper = userMapper ?? throw new ArgumentNullException(nameof(userMapper));
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

    public async Task<CompanyDto> AddCompanyWithUserAsync(CompanyDto company, UserDto user, CancellationToken cancellationToken)
    {
        using var uow = _unitOfWorkFactory.Create();
        {
            var userCompany = await uow.CompaniesStorage.AddAsync(new Company
            {
                CompanyName = company.CompanyName
            },
            cancellationToken);

            var newUser = _userMapper.Map(user, userCompany);
            await uow.UsersStorage.CreateAsync(newUser, cancellationToken);
            await uow.SaveChangesAsync();
            return _companyMapper.Map(userCompany);
        }
    }
}

