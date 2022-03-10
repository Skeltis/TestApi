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

    [HttpGet]
    [ProducesResponseType(typeof(UserModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<UserModel>> GetAsync([FromQuery][Required] Guid companyId, CancellationToken cancellationToken)
    {
        try
        {
            var company = await _companiesService.GetCompanyAsync(companyId, cancellationToken);
            return Ok(_companyMapper.Map(company));
        }
        catch (NotFoundException exception)
        {
            return NotFound(exception.Message);
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(UserModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<UserModel>> CreateAsync([Required] CreateCompanyRequest createCompanyRequest,
        CancellationToken cancellationToken)
    {
        var companyToAdd = _companyMapper.Map(createCompanyRequest);
        var addedCompany = await _companiesService.AddCompanyAsync(companyToAdd, cancellationToken);
        return Ok(_companyMapper.Map(addedCompany));
    }
}

