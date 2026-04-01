using ETradeBackend.Application.Abstractions.Services;
using ETradeBackend.Application.DTOs.Users;
using ETradeBackend.Domain.Entities.Identities;
using Microsoft.AspNetCore.Identity;

namespace ETradeBackend.Persistence.Services;

public class UserService(UserManager<AppUser> userManager) : IUserService
{
    public async Task<CreateUserResponse> CreateAsync(CreateUser user)
    {
        var result = await userManager.CreateAsync(new AppUser
        {
            Id = Guid.NewGuid(),
            NameSurname = user.NameSurname,
            UserName = user.Username,
            Email = user.Email
        }, user.Password);

        if (result.Succeeded)
            return new CreateUserResponse(true, "Kullanıcı başarıyla oluşturuldu.");
        
        var errors = string.Join(", ", result.Errors.Select(e => new
        {
            e.Description,
            e.Code
        }));
        return new CreateUserResponse(false, $"Kullanıcı oluşturulamadı: {errors}");
    }
}