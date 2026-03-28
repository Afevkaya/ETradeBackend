using ETradeBackend.Domain.Entities.Identities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETradeBackend.Application.Features.Commands.AppUsers.CreateUser;

public class CreateUserCommandHandler(UserManager<AppUser> userManager) : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
{
    public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await userManager.CreateAsync(new AppUser
        {
            Id = Guid.NewGuid(),
            NameSurname = request.NameSurname,
            UserName = request.Username,
            Email = request.Email
        }, request.Password);

        if (result.Succeeded)
            return new CreateUserCommandResponse(true, "Kullanıcı başarıyla oluşturuldu.");
        
        var errors = string.Join(", ", result.Errors.Select(e => new
        {
            e.Description,
            e.Code
        }));
        return new CreateUserCommandResponse(false, $"Kullanıcı oluşturulamadı: {errors}");
        
    }
}