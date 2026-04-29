using ETradeBackend.Application.Abstractions.Services.Configurations;
using ETradeBackend.Application.CustomAttributes;
using ETradeBackend.Application.DTOs.Configurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace ETradeBackend.Infrastructure.Services.Configurations;

public class ApplicationService : IApplicationService
{
    public List<Menu> GetAuthorizeDefinitionEndpoints(Type type)
    {
        var assembly = type.Assembly;
        var controllers = assembly.GetTypes().Where(x => x.IsAssignableTo(typeof(ControllerBase)));
        var menus = new List<Menu>();
        
        if (controllers == null) return [];
        
        foreach (var controller in controllers)
        {
            var actions = controller.GetMethods()
                .Where(x => x.IsDefined(typeof(AuthorizeDefinitionAttribute), true));
            
            if (actions == null) continue;
            
            foreach (var action in actions)
            {
                var attributes = action.GetCustomAttributes(true);
                if (attributes is not { Length: > 0 }) continue;
                Menu menu = null;
                var authorizeDefinitionAttribute =
                    attributes.FirstOrDefault(x => x.GetType() == typeof(AuthorizeDefinitionAttribute)) as
                        AuthorizeDefinitionAttribute;
                if(!menus.Any(x => x.Name == authorizeDefinitionAttribute.Menu))
                {
                    menu = new Menu { Name = authorizeDefinitionAttribute.Menu };
                    menus.Add(menu);
                }
                else
                {
                    menu = menus.FirstOrDefault(x => x.Name == authorizeDefinitionAttribute.Menu);
                }

                ETradeBackend.Application.DTOs.Configurations.Action actionDto = new()
                {
                    ActionType = Enum.GetName(authorizeDefinitionAttribute.ActionType),
                    Definition = authorizeDefinitionAttribute.Definition,
                    HttpType =
                        attributes.FirstOrDefault(x => x.GetType() == typeof(HttpMethodAttribute)) is
                            HttpMethodAttribute httpMethodAttribute
                            ? httpMethodAttribute.HttpMethods.FirstOrDefault()
                            : "GET",
                };
                actionDto.Code = $"{actionDto.HttpType}.{actionDto.ActionType}.{actionDto.Definition}";
                menu?.Actions.Add(actionDto);
            }
        }
       
        return menus;
    }
}