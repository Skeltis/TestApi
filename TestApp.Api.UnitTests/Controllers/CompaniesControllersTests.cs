using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestApp.Api.Contracts.v1.Model;
using TestApp.Api.Controllers.v1;
using TestApp.Api.Mappers;
using TestApp.BLL.Contracts.Dtos;
using TestApp.BLL.Contracts.Exceptions;
using TestApp.BLL.Services;
using TestApp.BLL.UnitTests.Services;
using Xunit;

namespace TestApp.Api.UnitTests.Controllers
{
    public class CompaniesControllersTests
    {
        private CompaniesController ControllerWithSuccess()
        {
            var companiesService = new DbServiceTestFixture().CreateCompaniesServiceMock();
            companiesService.Setup(x => x.AddCompanyAsync(It.IsAny<CompanyDto>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((CompanyDto comp, CancellationToken token) => comp);

            companiesService.Setup(x => x.AddCompanyWithUserAsync(It.IsAny<CompanyDto>(), It.IsAny<UserDto>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((CompanyDto comp, UserDto user, CancellationToken token) =>
                {
                    comp.Users = new List<UserDto>();
                    comp.Users.Add(user);
                    return comp;
                });

            var companyMapper = new CompanyMapper();
            return new CompaniesController(companiesService.Object, companyMapper);
        }

        private CompaniesController ControllerWithConflict()
        {
            var companiesService = new DbServiceTestFixture().CreateCompaniesServiceMock();
            companiesService.Setup(x => x.AddCompanyAsync(It.IsAny<CompanyDto>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new AlreadyExistsException());

            companiesService.Setup(x => x.AddCompanyWithUserAsync(It.IsAny<CompanyDto>(), It.IsAny<UserDto>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new AlreadyExistsException());

            var companyMapper = new CompanyMapper();
            return new CompaniesController(companiesService.Object, companyMapper);
        }

        [Fact]
        public async Task CompaniesController_CreateEmptyCompany_ReturnsSuccess()
        {
            var controller = ControllerWithSuccess();
            var request = new CreateCompanyRequest()
            {
                CompanyName = "Test Company"
            };
            var result = await controller.CreateAsync(request, CancellationToken.None);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task CompaniesController_CreateCompanyWithUser_ReturnsSuccess()
        {
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
            var result = await controller.CreateAsync(request, CancellationToken.None);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task CompaniesController_CreateCompanyWithEmptyUser_ReturnsConflictt()
        {
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
            var result = await controller.CreateAsync(request, CancellationToken.None);
            Assert.NotNull(result);
            Assert.IsType<ConflictObjectResult>(result.Result);
        }
    }
}
