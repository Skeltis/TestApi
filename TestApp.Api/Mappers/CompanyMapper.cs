using TestApp.Api.Contracts.Interfaces.Mappers;
using TestApp.Api.Contracts.v1.Model;
using TestApp.BLL.Contracts.Dtos;

namespace TestApp.Api.Mappers;

public class CompanyMapper : ICompanyMapper
{
    public (CompanyDto company, UserDto? user) Map(CreateCompanyRequest request)
    {
        UserDto user = null;
        var company = new CompanyDto
        {
            CompanyName = request.CompanyName
        };

        if (request.User != null)
        {
            user = new UserDto
            {
                Email = request.User.Email,
                Name = request.User.Name,
                PasswordHash = request.User.PasswordHash
            };
        }

        return (company, user);
    }

    public CompanyModel Map(CompanyDto user)
    {
        return new CompanyModel
        {
            Id = user.Id,
            CompanyName = user.CompanyName
        };
    }
}

