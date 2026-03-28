using Microsoft.AspNetCore.Identity;

namespace ETradeBackend.Domain.Entities.Identities;

public class AppUser : IdentityUser<Guid>
{
    public string NameSurname { get; set; }
}