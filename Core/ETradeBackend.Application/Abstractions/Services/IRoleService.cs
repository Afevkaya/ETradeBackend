namespace ETradeBackend.Application.Abstractions.Services;

public interface IRoleService
{
    Task<bool> CreateAsync(string name);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> UpdateAsync(Guid id, string name);
    Task<(object datas, int totalCount)> GetAllAsync(int page, int pageSize);
    Task<(Guid Id, string Name)?> GetByIdAsync(Guid id);
}