using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using TestApp.Api.Contracts.v1.Model;
using Xunit;

namespace TestApp.Api.UnitTests.Controllers
{
    public class CompaniesControllersTests : CompaniesControllerTestBase
    {
        [Fact]
        public async Task CompaniesController_CreateEmptyCompany_ReturnsSuccess()
        {
            //arrange
            var controller = ControllerWithSuccess();
            var request = new CreateCompanyRequest()
            {
                CompanyName = "Test Company"
            };

            //act
            var result = await controller.CreateAsync(request, CancellationToken.None);

            //assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task CompaniesController_CreateCompanyWithUser_ReturnsSuccess()
        {
            //arrange
            var controller = ControllerWithSuccess();
            var request = new CreateCompanyRequest()
            {
                CompanyName = "Test Company",
                User = new CreateUserRequest
                {
                    Name = "TestUser",
                    Email = "test@user.com",
                    PasswordHash = "hash"
                }
            };

            //act
            var result = await controller.CreateAsync(request, CancellationToken.None);

            //assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task CompaniesController_CreateCompanyWithEmptyUser_ReturnsConflictt()
        {
            //arrange
            var controller = ControllerWithConflict();
            var request = new CreateCompanyRequest()
            {
                CompanyName = "Test Company",
                User = new CreateUserRequest
                {
                    Name = "TestUser",
                    Email = "test@user.com",
                    PasswordHash = "hash"
                }
            };

            //act
            var result = await controller.CreateAsync(request, CancellationToken.None);

            //assert
            Assert.NotNull(result);
            Assert.IsType<ConflictObjectResult>(result.Result);
        }
    }
}
