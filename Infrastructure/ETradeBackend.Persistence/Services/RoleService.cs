using ETradeBackend.Application.Abstractions.Services;
using ETradeBackend.Domain.Entities.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ETradeBackend.Persistence.Services;

public class RoleService(RoleManager<AppRole> roleManager) : IRoleService
{
    public async Task<bool> CreateAsync(string name)
    {
        var result = await roleManager.CreateAsync(new AppRole { Id = Guid.NewGuid(), Name = name });
        return result.Succeeded;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var role = await roleManager.FindByIdAsync(id.ToString());
        if (role == null) return false;
        var result = await roleManager.DeleteAsync(role);
        return result.Succeeded;
    }

    public async Task<bool> UpdateAsync(Guid id, string name)
    {
        var role = await roleManager.FindByIdAsync(id.ToString());
        if (role == null) return false;
        role.Name = name;
        var result = await roleManager.UpdateAsync(role);
        return result.Succeeded;
    }

    public async Task<(object datas, int totalCount)> GetAllAsync(int page, int pageSize)
    {
        var query = roleManager.Roles.Select(r => new { r.Id, r.Name });
        var totalCount = await query.CountAsync();
        var datas = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return (datas, totalCount);
    }

    public async Task<(Guid Id, string Name)?> GetByIdAsync(Guid id)
    {
        var data = await roleManager.Roles.Where(r => r.Id == id)
            .Select(r => new { r.Id, r.Name }).FirstOrDefaultAsync();
        return (data.Id, data.Name);
    }
}