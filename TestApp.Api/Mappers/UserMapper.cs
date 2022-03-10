using TestApp.Api.Contracts.Interfaces.Mappers;
using TestApp.Api.Contracts.v1.Model;
using TestApp.BLL.Contracts.Dtos;

namespace TestApp.Api.Mappers;

public class UserMapper : IUserMapper
{
    public UserDto Map(CreateUserUnderCompanyRequest request)
    {
        return new UserDto
        {
            Email = request.Email,
            Name = request.Name,
            PasswordHash = request.PasswordHash,
            Company = new CompanyDto
            {
                Id = request.CompanyId
            }
        };
    }

    public UserModel Map(UserDto user)
    {
        return new UserModel
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            PasswordHash = user.PasswordHash,
            CompanyId = user.Company.Id
        };
    }
}

