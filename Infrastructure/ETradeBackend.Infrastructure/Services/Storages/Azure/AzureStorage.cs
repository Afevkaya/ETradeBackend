using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using ETradeBackend.Application.Abstractions.Storages.Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ETradeBackend.Infrastructure.Services.Storages.Azure;

public class AzureStorage : Storage, IAzureStorage
{
    private readonly BlobServiceClient _blobServiceClient;
    BlobContainerClient _blobContainerClient;

    public AzureStorage(IConfiguration configuration)
    {
        _blobServiceClient = new BlobServiceClient(configuration["Storage:Azure"]);
    }
    
    public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string containerName, IFormFileCollection files)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        await _blobContainerClient.CreateIfNotExistsAsync();
        await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);
        var fileList = new List<(string fileName, string pathOrContainerName)>();
        foreach (var file in files)
        {
            var fileNewName = await FileRenameAsync(containerName, file.FileName, HasFile);
            var blobClient = _blobContainerClient.GetBlobClient(fileNewName);
            await blobClient.UploadAsync(file.OpenReadStream());
            fileList.Add((fileNewName, containerName));
        }
        return fileList;
    }

    public Task DeleteAsync(string containerName, string fileName)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = _blobContainerClient.GetBlobClient(fileName);
        return blobClient.DeleteIfExistsAsync();
    }

    public List<string> GetFiles(string containerName)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobs = _blobContainerClient.GetBlobs();
        return blobs.Select(b => b.Name).ToList();
    }

    public bool HasFile(string containerName, string fileName)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = _blobContainerClient.GetBlobClient(fileName);
        return blobClient.Exists();
    }
}