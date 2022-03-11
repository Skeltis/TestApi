using Moq;
using System.Collections.Generic;
using System.Threading;
using TestApp.Api.Controllers.v1;
using TestApp.Api.Mappers;
using TestApp.BLL.Contracts.Dtos;
using TestApp.BLL.Contracts.Exceptions;
using TestApp.BLL.Contracts.Interfaces;

namespace TestApp.Api.UnitTests.Controllers
{
    public abstract class CompaniesControllerTestBase
    {
        protected static CompaniesController ControllerWithSuccess()
        {
            var companiesService = new Mock<ICompaniesService>();
            companiesService.Setup(x => x.AddCompanyAsync(It.IsAny<CompanyDto>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((CompanyDto comp, CancellationToken token) => comp);

            companiesService.Setup(x => x.AddCompanyWithUserAsync(It.IsAny<CompanyDto>(), It.IsAny<UserDto>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((CompanyDto comp, UserDto user, CancellationToken token) =>
                {
                    comp.Users = new List<UserDto>
                    {
                        user
                    };
                    return comp;
                });

            var companyMapper = new CompanyMapper();
            return new CompaniesController(companiesService.Object, companyMapper);
        }

        protected static CompaniesController ControllerWithConflict()
        {
            var companiesService = new Mock<ICompaniesService>();
            companiesService.Setup(x => x.AddCompanyAsync(
                    It.IsAny<CompanyDto>(), 
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new AlreadyExistsException());

            companiesService.Setup(x => x.AddCompanyWithUserAsync(
                    It.IsAny<CompanyDto>(), 
                    It.IsAny<UserDto>(), 
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new AlreadyExistsException());

            var companyMapper = new CompanyMapper();
            return new CompaniesController(companiesService.Object, companyMapper);
        }
    }
}
