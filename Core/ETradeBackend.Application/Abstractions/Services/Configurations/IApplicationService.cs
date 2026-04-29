using ETradeBackend.Application.DTOs.Configurations;

namespace ETradeBackend.Application.Abstractions.Services.Configurations;

public interface IApplicationService
{
    List<Menu> GetAuthorizeDefinitionEndpoints(Type type);
}