using ETradeBackend.Application.DTOs;
using ETradeBackend.Application.DTOs.Tokens;

namespace ETradeBackend.Application.Features.Commands.AppUsers.LoginUser;

public record LoginUserCommandResponse(Token Token);