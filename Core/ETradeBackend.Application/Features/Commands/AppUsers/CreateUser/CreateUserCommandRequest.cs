using MediatR;

namespace ETradeBackend.Application.Features.Commands.AppUsers.CreateUser;

public record CreateUserCommandRequest(string NameSurname,string Username, string Email, string Password, string PasswordConfirm) : IRequest<CreateUserCommandResponse>;