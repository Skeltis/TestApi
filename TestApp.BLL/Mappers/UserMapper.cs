using TestApp.BLL.Contracts.Dtos;
using TestApp.BLL.Contracts.Mappers;
using TestApp.Data.Contracts.Models;

namespace TestApp.BLL.Mappers;

public class UserMapper : IUserMapper
{
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

