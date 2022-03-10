using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestApp.BLL.Contracts.Dtos;
using TestApp.BLL.Mappers;
using TestApp.BLL.Services;
using TestApp.Data;
using TestApp.Data.Base;

namespace TestApp.BLL.UnitTests.Services;

public class DbServiceTestFixture
{
    private CompaniesService _companiesService;

    private UnitOfWorkFactory _unitOfWorkFactory;

    public DbServiceTestFixture()
    {
        var mockDbFactory = new Mock<IDbContextFactory<MainDbContext>>();
        var dbContextOptionsBuilder = new DbContextOptionsBuilder<MainDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString());
        mockDbFactory.Setup(f => f.CreateDbContext())
            .Returns(() => new MainDbContext(dbContextOptionsBuilder
            .Options));

        _unitOfWorkFactory = new(mockDbFactory.Object);
        _companiesService = new CompaniesService(_unitOfWorkFactory, 
            new CompanyMapper(), new UserMapper(), 
            NullLogger<CompaniesService>.Instance);
    }

    public Mock<CompaniesService> CreateCompaniesServiceMock()
    {
        var mockDbFactory = new Mock<IDbContextFactory<MainDbContext>>();
        var dbContextOptionsBuilder = new DbContextOptionsBuilder<MainDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString());
        mockDbFactory.Setup(f => f.CreateDbContext())
            .Returns(() => new MainDbContext(dbContextOptionsBuilder
            .Options));

        _unitOfWorkFactory = new(mockDbFactory.Object);
        return new Mock<CompaniesService>(_unitOfWorkFactory, 
            new CompanyMapper(), new UserMapper(),
            NullLogger<CompaniesService>.Instance);
    }

    public Task<CompanyDto> TestCreateCompanyAsync(CompanyDto companyDto)
    {
        return _companiesService.AddCompanyAsync(companyDto, CancellationToken.None);
    }

    public Task<CompanyDto> TestCreateCompanyWithUserAsync(CompanyDto companyDto, UserDto userDto)
    {
        return _companiesService.AddCompanyWithUserAsync(companyDto, userDto, CancellationToken.None);
    }
}

