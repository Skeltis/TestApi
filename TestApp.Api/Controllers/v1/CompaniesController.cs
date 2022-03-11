using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using TestApp.Api.Contracts;
using TestApp.Api.Contracts.Interfaces.Mappers;
using TestApp.Api.Contracts.v1.Model;
using TestApp.BLL.Contracts.Exceptions;
using TestApp.BLL.Contracts.Interfaces;

namespace TestApp.Api.Controllers.v1;

[Route("api/[controller]")]
[ApiController]
public class CompaniesController : Controller
{
    private readonly ICompaniesService _companiesService;
    private readonly ICompanyMapper _companyMapper;

    public CompaniesController(ICompaniesService companiesService, ICompanyMapper companyMapper)
    {
        _companiesService = companiesService ?? throw new ArgumentNullException(nameof(companiesService));
        _companyMapper = companyMapper ?? throw new ArgumentNullException(nameof(companyMapper));
    }

    [HttpPost]
    [ProducesResponseType(typeof(CompanyModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.Conflict)]
    public async Task<ActionResult<CompanyModel>> CreateAsync([Required] CreateCompanyRequest createCompanyRequest,
        CancellationToken cancellationToken)
    {
        (var companyToAdd, var userToAdd) = _companyMapper.Map(createCompanyRequest);
        try
        {
            var addedCompany = (userToAdd != null)
                ? await _companiesService.AddCompanyWithUserAsync(companyToAdd, userToAdd, cancellationToken)
                : await _companiesService.AddCompanyAsync(companyToAdd, cancellationToken);
            return Ok(_companyMapper.Map(addedCompany));
        }
        catch (AlreadyExistsException exception)
        {
            return Conflict(exception.Message);
        }
    }
}

