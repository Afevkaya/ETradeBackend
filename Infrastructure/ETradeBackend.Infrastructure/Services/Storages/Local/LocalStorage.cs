using ETradeBackend.Application.Abstractions.Storages.Local;
using ETradeBackend.Infrastructure.Operations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ETradeBackend.Infrastructure.Services.Storages.Local;

public class LocalStorage(IWebHostEnvironment webHostEnvironment) : Storage, ILocalStorage
{
    public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string path, IFormFileCollection files)
    {
        // Gelen path ile wwwroot 'u birleştiriyoruz. Örneğin: wwwroot/images/products
        var uploadPath = Path.Combine(webHostEnvironment.WebRootPath, path);
        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);
        
        var fileList = new List<(string fileName, string path)>();
        foreach (var file in files)
        {
            var fileNewName = await FileRenameAsync(path, file.FileName, HasFile);
            await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);
            fileList.Add((fileNewName, $"{path}\\{fileNewName}"));
        }
        
        // TODO: Aşağıdaki koşulda custom exception ele alınacak
        return fileList;
    }

    public async Task DeleteAsync(string path, string fileName) => File.Delete($"{path}\\{fileName}");

    public List<string> GetFiles(string path)
    {
        DirectoryInfo directory = new DirectoryInfo(path);
        return directory.GetFiles().Select(x => x.Name).ToList();
    }

    public bool HasFile(string path, string fileName) => File.Exists($"{path}\\{fileName}");
    
    private async Task CopyFileAsync(string path, IFormFile file)
    {
        try
        {
            // Gelen path'i kullanarak dosyayı oluşturuyoruz ve dosya akışını açıyoruz. Daha sonra gelen dosyayı bu akışa kopyalıyoruz.
            await using var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write,FileShare.None, 1024 * 1024, useAsync: false);
            await file.CopyToAsync(fileStream);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}