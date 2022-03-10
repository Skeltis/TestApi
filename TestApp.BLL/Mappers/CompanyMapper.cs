using TestApp.BLL.Contracts.Dtos;
using TestApp.BLL.Contracts.Mappers;
using TestApp.Data.Contracts.Models;

namespace TestApp.BLL.Mappers;

public class CompanyMapper : ICompanyMapper
{
    public CompanyDto Map(Company company)
    {
        var companyDto = new CompanyDto
        {
            Id = company.Id,
            CompanyName = company.CompanyName
        };

        if (company.Users != null)
        {
            var users = company.Users
                .Select(x => new UserDto
                {
                    Id = x.Id,
                    Email = x.Email,
                    Name = x.Name,
                    PasswordHash = x.PasswordHash,
                    Company = companyDto
                })
                .ToArray();

            companyDto.Users = users;
        }

        return companyDto;
    }

    public Company Map(CompanyDto companyDto)
    {
        var company = new Company
        {
            Id = companyDto.Id,
            CompanyName = companyDto.CompanyName
        };

        if (companyDto.Users != null)
        {
            var users = companyDto.Users
                .Select(x => new User
                {
                    Id = x.Id,
                    Email = x.Email,
                    Name = x.Name,
                    PasswordHash = x.PasswordHash,
                    Company = company
                })
                .ToArray();

            company.Users = users;
        }

        return company;
    }
}

