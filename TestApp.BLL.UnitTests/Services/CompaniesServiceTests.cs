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
        //arrange
        var company = new CompanyDto
        {
            CompanyName = "TestCompany"
        };
        var fixture = new DbServiceTestFixture();

        //act
        await fixture.TestCreateCompanyAsync(company);

        //assert
        await Assert.ThrowsAsync<AlreadyExistsException>(async () => await fixture.TestCreateCompanyAsync(company));
    }

    [Fact]
    public async Task CreateCompanySameNameWithUser_ThrowsAlreadyExistsException()
    {
        //arrange
        var company = new CompanyDto
        {
            CompanyName = "TestCompany"
        };
        var fixture = new DbServiceTestFixture();

        //act
        await fixture.TestCreateCompanyWithUserAsync(company,
            new UserDto
            {
                Email = "a@test.com",
                Name = "Mr.Test",
                PasswordHash = "Hash"
            });

        //assert
        await Assert.ThrowsAsync<AlreadyExistsException>(async () =>
            await fixture.TestCreateCompanyWithUserAsync(company,
                new UserDto
                {
                    Email = "a@test2.com",
                    Name = "Mr.Test2",
                    PasswordHash = "Hash2"
                }));
    }
}

