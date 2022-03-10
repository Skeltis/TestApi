using TestApp.BLL.Contracts.Dtos;
using TestApp.Data.Contracts.Models;

namespace TestApp.BLL.Contracts.Mappers;

public interface IUserMapper
{
    UserDto Map(User user);
    User Map(UserDto user, Company userCompany);
}
