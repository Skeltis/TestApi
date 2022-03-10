﻿using TestApp.BLL.Contracts.Dtos;

namespace TestApp.BLL.Contracts.Interfaces;

public interface IUsersService
{
    Task<UserDto> AddUserUnderCompanyAsync(UserDto user, CancellationToken cancellationToken);
    Task<UserDto> GetUserAsync(Guid userId, CancellationToken cancellationToken);
}
