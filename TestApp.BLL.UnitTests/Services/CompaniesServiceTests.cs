using System.Threading.Tasks;
using TestApp.BLL.Contracts.Dtos;
using TestApp.BLL.Contracts.Exceptions;
using Xunit;

namespace TestApp.BLL.UnitTests.Services;

public class CompaniesServiceTests
{
    [Fact]
    public async Task CreateCompanyWithSameName_ThrowsAlreadyExistsException()
    {
        var company = new CompanyDto
        {
            CompanyName = "TestCompany"
        };

        var fixture = new DbServiceTestFixture();
        await fixture.TestCreateCompanyAsync(company);
        await Assert.ThrowsAsync<AlreadyExistsException>(async () => await fixture.TestCreateCompanyAsync(company));
    }

    [Fact]
    public async Task CreateCompanySameNameWithUser_ThrowsAlreadyExistsException()
    {
        var company = new CompanyDto
        {
            CompanyName = "TestCompany"
        };

        var fixture = new DbServiceTestFixture();
        await fixture.TestCreateCompanyWithUserAsync(company, 
            new UserDto
            {
                Company = company,
                Email = "a@test.com",
                Name = "Mr.Test",
                PasswordHash = "Hash"
            });
        await Assert.ThrowsAsync<AlreadyExistsException>(async () => 
            await fixture.TestCreateCompanyWithUserAsync(company,
                new UserDto
                {
                    Company = company,
                    Email = "a@test2.com",
                    Name = "Mr.Test2",
                    PasswordHash = "Hash2"
                }));
    }

    [Fact]
    public async Task CreateCompanyWithUserSameEmail_ThrowsAlreadyExistsException()
    {
        var company = new CompanyDto
        {
            CompanyName = "TestCompany"
        };
        var user = new UserDto
        {
            Company = company,
            Email = "a@test.com",
            Name = "Mr.Test",
            PasswordHash = "Hash"
        };

        var fixture = new DbServiceTestFixture();
        await fixture.TestCreateCompanyWithUserAsync(company, user);
        company.CompanyName = "TestCompany_2";
        await Assert.ThrowsAsync<AlreadyExistsException>(async () =>
            await fixture.TestCreateCompanyWithUserAsync(company, user));
    }
}

