using TestApp.BLL.Contracts.Dtos;
using TestApp.Data.Contracts.Models;

namespace TestApp.BLL.Contracts.Mappers;

public interface ICompanyMapper
{
    CompanyDto Map(Company company);
    Company Map(CompanyDto companyDto);
}
