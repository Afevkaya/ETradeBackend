using ETradeBackend.Application.DTOs;
using ETradeBackend.Application.DTOs.Tokens;

namespace ETradeBackend.Application.Features.Commands.AppUsers.RefreshTokenLogin;

public record RefreshTokenLoginCommandResponse(Token Token);