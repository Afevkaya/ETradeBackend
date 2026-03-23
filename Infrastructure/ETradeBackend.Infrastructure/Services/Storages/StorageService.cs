using ETradeBackend.Application.Abstractions.Storages;
using Microsoft.AspNetCore.Http;

namespace ETradeBackend.Infrastructure.Services.Storages;

public class StorageService(IStorage storage) : IStorageService
{
    public string StorageName => storage.GetType().Name;

    public Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files) 
        => storage.UploadAsync(pathOrContainerName, files);
    public Task DeleteAsync(string pathOrContainerName, string fileName) => storage.DeleteAsync(pathOrContainerName, fileName);
    public List<string> GetFiles(string pathOrContainerName) => storage.GetFiles(pathOrContainerName);
    public bool HasFile(string pathOrContainerName, string fileName) => storage.HasFile(pathOrContainerName, fileName);
    
}