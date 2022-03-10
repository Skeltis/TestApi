using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
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

    public virtual async Task<CompanyDto> AddCompanyAsync(CompanyDto company, CancellationToken cancellationToken)
    {
        using var uow = _unitOfWorkFactory.Create();
        {
            var companyEntity = _companyMapper.Map(company);
            var existingEntity = await uow.CompaniesStorage.GetAsync(company.CompanyName, cancellationToken);
            if (existingEntity != null)
            {
                Logger.LogError($"Company with name={company.CompanyName} already exists");
                throw new AlreadyExistsException($"Company with name={company.CompanyName} already exists");
            }

            var addedCompany = await uow.CompaniesStorage.AddAsync(companyEntity, cancellationToken);
            try
            {
                await uow.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Logger.LogError($"Company with name={company.CompanyName} already exists");
                throw new AlreadyExistsException($"Company with name={company.CompanyName} already exists", ex);
            }
            return _companyMapper.Map(addedCompany);
        }
    }

    public virtual async Task<CompanyDto> AddCompanyWithUserAsync(CompanyDto company, UserDto user, CancellationToken cancellationToken)
    {
        using var uow = _unitOfWorkFactory.Create();
        {
            var existingCompany = await uow.CompaniesStorage.GetAsync(company.CompanyName, cancellationToken);
            if (existingCompany != null)
            {
                var message = $"Company with name={existingCompany.CompanyName} already exists";
                Logger.LogError(message);
                throw new AlreadyExistsException(message);
            }
            var existingUser = await uow.UsersStorage.GetAsync(user.Email, cancellationToken);
            if (existingUser != null)
            {
                var message = existingUser.CompanyId == company.Id
                    ? $"User with email={user.Email} already exists under current company {company.Id}"
                    : $"User with email={ user.Email } already exists under different company {existingUser.CompanyId}";
                Logger.LogError(message);
                throw new AlreadyExistsException(message);
            }

            var addedCompany = await uow.CompaniesStorage.AddAsync(new Company { CompanyName = company.CompanyName },
                cancellationToken);

            var newUser = _userMapper.Map(user, addedCompany);
            await uow.UsersStorage.CreateAsync(newUser, cancellationToken);
            try
            {
                await uow.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Logger.LogError($"Company with name={company.CompanyName} already exists");
                throw new AlreadyExistsException($"Company with name={company.CompanyName} already exists", ex);
            }
            return _companyMapper.Map(addedCompany);
        }
    }
}

