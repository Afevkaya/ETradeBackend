using ETradeBackend.Application.DTOs.Users;

namespace ETradeBackend.Application.Abstractions.Services;

public interface IUserService
{
    Task<CreateUserResponse> CreateAsync(CreateUser user);
}