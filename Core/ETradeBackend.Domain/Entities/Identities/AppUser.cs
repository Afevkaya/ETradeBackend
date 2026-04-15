using ETradeBackend.Domain.Entities.Baskets;
using Microsoft.AspNetCore.Identity;

namespace ETradeBackend.Domain.Entities.Identities;

public class AppUser : IdentityUser<Guid>
{
    public string NameSurname { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiration { get; set; }
    public ICollection<Basket> Baskets { get; set; }
}