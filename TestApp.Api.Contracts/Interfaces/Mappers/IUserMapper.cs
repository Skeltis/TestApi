using TestApp.Api.Contracts.v1.Model;
using TestApp.BLL.Contracts.Dtos;

namespace TestApp.Api.Contracts.Interfaces.Mappers;

public interface IUserMapper
{
    UserDto Map(CreateUserUnderCompanyRequest request);
    UserModel Map(UserDto user);
}
