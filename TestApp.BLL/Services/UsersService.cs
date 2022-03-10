using Microsoft.Extensions.Logging;
using TestApp.BLL.Contracts.Dtos;
using TestApp.BLL.Contracts.Exceptions;
using TestApp.BLL.Contracts.Interfaces;
using TestApp.BLL.Contracts.Mappers;
using TestApp.Data.Contracts.Interfaces;
using TestApp.Data.Contracts.Models;

namespace TestApp.BLL.Services;

public class UsersService : BaseService<UsersService>, IUsersService
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly IUserMapper _userMapper;

    public UsersService(IUnitOfWorkFactory unitOfWorkFactory, IUserMapper userMapper,
        ILogger<UsersService> logger)
        : base(logger)
    {
        _unitOfWorkFactory = unitOfWorkFactory ?? throw new ArgumentNullException(nameof(unitOfWorkFactory));
        _userMapper = userMapper ?? throw new ArgumentNullException(nameof(userMapper));
    }

    public async Task<UserDto> GetUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        using var uow = _unitOfWorkFactory.Create();
        {
            var user = await uow.UsersStorage.GetAsync(userId, cancellationToken);
            if (user == null)
            {
                Logger.LogError($"User with id={userId} doesn't exist");
                throw new NotFoundException($"User with id={userId} doesn't exist");
            }

            return _userMapper.Map(user);
        }
    }

    public async Task<UserDto> AddUserWithCompanyAsync(UserDto user, CancellationToken cancellationToken)
    {
        using var uow = _unitOfWorkFactory.Create();
        {
            var userCompany = await uow.CompaniesStorage.AddAsync(new Company
            {
                CompanyName = user.Company.CompanyName
            },
            cancellationToken);

            var newUser = _userMapper.Map(user, userCompany);
            var createdUser = await uow.UsersStorage.CreateAsync(newUser, cancellationToken);
            await uow.SaveChangesAsync();
            return _userMapper.Map(createdUser);
        }
    }

    public async Task<UserDto> AddUserUnderCompanyAsync(UserDto user, CancellationToken cancellationToken)
    {
        using var uow = _unitOfWorkFactory.Create();
        {
            var userCompany = await uow.CompaniesStorage.GetAsync(user.Company.Id, cancellationToken);
            if (userCompany == null)
            {
                Logger.LogError($"Company with id={user.Company.Id} doesn't exist");
                throw new NotFoundException($"Company with id={user.Company.Id} doesn't exist");
            }
            var newUser = _userMapper.Map(user, userCompany);
            var createdUser = await uow.UsersStorage.CreateAsync(newUser, cancellationToken);
            await uow.SaveChangesAsync();
            return _userMapper.Map(createdUser);
        }
    }
}

