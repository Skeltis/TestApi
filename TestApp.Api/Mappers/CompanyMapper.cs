using TestApp.Api.Contracts.Interfaces.Mappers;
using TestApp.Api.Contracts.v1.Model;
using TestApp.BLL.Contracts.Dtos;

namespace TestApp.Api.Mappers;

public class CompanyMapper : ICompanyMapper
{
    public CompanyDto Map(CreateCompanyRequest request)
    {
        return new CompanyDto
        {
            CompanyName = request.CompanyName
        };
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

