using MediatR;

namespace ETradeBackend.Application.Features.Commands.AppUsers.GoogleLogin;

public record GoogleLoginCommandRequest(string Id, string IdToken, string Name, string FirstName, string LastName, string Email, string PhotoUrl, string Provider) : IRequest<GoogleLoginCommandResponse>;