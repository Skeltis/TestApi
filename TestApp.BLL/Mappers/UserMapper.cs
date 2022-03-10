using TestApp.BLL.Contracts.Dtos;
using TestApp.BLL.Contracts.Mappers;
using TestApp.Data.Contracts.Models;

namespace TestApp.BLL.Mappers;

public class UserMapper : IUserMapper
{
    public UserDto Map(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name,
            PasswordHash = user.PasswordHash,
            Company = new CompanyDto
            {
                Id = user.Company.Id,
                CompanyName = user.Company.CompanyName
            }
        };
    }

    public User Map(UserDto user, Company userCompany)
    {
        return new User
        {
            Name = user.Name,
            Email = user.Email,
            PasswordHash = user.PasswordHash,
            CompanyId = userCompany.Id,
            Company = userCompany
        };
    }
}

