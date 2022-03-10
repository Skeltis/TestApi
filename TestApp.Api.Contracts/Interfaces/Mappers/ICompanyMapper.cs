using TestApp.Api.Contracts.v1.Model;
using TestApp.BLL.Contracts.Dtos;

namespace TestApp.Api.Contracts.Interfaces.Mappers;

public interface ICompanyMapper
{
    CompanyModel Map(CompanyDto user);
    CompanyDto Map(CreateCompanyRequest request);
}
