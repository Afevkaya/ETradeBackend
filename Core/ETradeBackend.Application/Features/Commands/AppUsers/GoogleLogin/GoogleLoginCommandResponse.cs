using ETradeBackend.Application.DTOs;
using ETradeBackend.Application.DTOs.Tokens;

namespace ETradeBackend.Application.Features.Commands.AppUsers.GoogleLogin;

public record GoogleLoginCommandResponse(Token Token);