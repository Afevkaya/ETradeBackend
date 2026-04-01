using ETradeBackend.Application.Abstractions.Services;
using MediatR;

namespace ETradeBackend.Application.Features.Commands.AppUsers.CreateUser;

public class CreateUserCommandHandler(IUserService userService) : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
{
    public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await userService.CreateAsync(new DTOs.Users.CreateUser(request.NameSurname, request.Username, request.Email, request.Password, request.PasswordConfirm));
        return new CreateUserCommandResponse(result.IsSuccess, result.Message);
    }
}