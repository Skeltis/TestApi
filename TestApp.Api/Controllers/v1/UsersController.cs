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
public class UsersController : Controller
{
    private readonly IUsersService _usersService;
    private readonly IUserMapper _userMapper;

    public UsersController(IUsersService usersService, IUserMapper userMapper)
    {
        _usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
        _userMapper = userMapper ?? throw new ArgumentNullException(nameof(userMapper));
    }

    [HttpGet]
    [ProducesResponseType(typeof(UserModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<UserModel>> GetAsync([FromQuery][Required] Guid userId, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _usersService.GetUserAsync(userId, cancellationToken);
            return Ok(_userMapper.Map(user));
        }
        catch (NotFoundException exception)
        {
            return NotFound(exception.Message);
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(UserModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<UserModel>> CreateAsync([Required] CreateUserWithCompanyRequest createUserRequest,
        CancellationToken cancellationToken)
    {
        var userToAdd = _userMapper.Map(createUserRequest);
        var addedUser = await _usersService.AddUserWithCompanyAsync(userToAdd, cancellationToken);
        return Ok(_userMapper.Map(addedUser));
    }

    [HttpPut]
    [ProducesResponseType(typeof(UserModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<UserModel>> PutAsync([Required] CreateUserUnderCompanyRequest createUserRequest,
    CancellationToken cancellationToken)
    {
        try
        {
            var userToAdd = _userMapper.Map(createUserRequest);
            var addedUser = await _usersService.AddUserUnderCompanyAsync(userToAdd, cancellationToken);
            return Ok(_userMapper.Map(addedUser));
        }
        catch (NotFoundException exception)
        {
            return NotFound(exception.Message);
        }
    }
}

