using ETradeBackend.Application.Services;
using ETradeBackend.Infrastructure.Operations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ETradeBackend.Infrastructure.Services;

public class FileService(IWebHostEnvironment webHostEnvironment) : IFileService
{
    public async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files)
    {
        // Gelen path ile wwwroot 'u birleştiriyoruz. Örneğin: wwwroot/images/products
        var uploadPath = Path.Combine(webHostEnvironment.WebRootPath, path);
        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);
        
        var fileList = new List<(string fileName, string path)>();
        var results = new List<bool>();
        foreach (var file in files)
        {
            var fileNewName = await FileRenameAsync(uploadPath,file.FileName);
            var result = await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);
            fileList.Add((fileNewName, $"{uploadPath}\\{fileNewName}"));
            results.Add(result);
        }
        
        // TODO: Aşağıdaki koşulda custom exception ele alınacak
        return results.TrueForAll(x => x.Equals(true)) ? fileList : null;
    }

    private async Task<string> FileRenameAsync(string path, string fileName)
    {
        var newFileName = await Task.Run(async () => 
        {
            // gelen filename'in uzantısını alıyoruz. Örneğin: "product-image.jpg" -> uzantı: ".jpg", "document.pdf" -> uzantı: ".pdf"
            var extension = Path.GetExtension(fileName);
            // gelen filename 'in sadece dosya adını alıyoruz. Örneğin: "product-image.jpg" -> sadece dosya adı: "product-image"
            var onlyFileName = Path.GetFileNameWithoutExtension(fileName);
            // dosya adını karakter düzenlemesine tabi tutarak yeni bir dosya adı oluşturuyoruz. Örneğin: "product_image" -> "productimage.jpg"
            var newFileName = NameOperation.CharacterRegulatory(onlyFileName) + extension;

            // Aynı isimde bir dosya var mı kontrol ediyoruz. Eğer varsa, dosya adının sonuna "-1", "-2" gibi sayılar ekleyerek yeni bir dosya adı oluşturuyoruz.
            // Örneğin: "productimage.jpg" -> "productimage-1.jpg", "productimage-2.jpg" vb.
            var fileCounter = 1;
            while (File.Exists(Path.Combine(path,newFileName))) 
            {
                fileCounter++;
                newFileName = NameOperation.CharacterRegulatory(onlyFileName) + "-" + fileCounter + extension;

                if (fileCounter == int.MaxValue)
                    break;
                
            }
            return newFileName;
        });
        return newFileName;
    }

    private async Task<bool> CopyFileAsync(string path, IFormFile file)
    {
        try
        {
            // Gelen path'i kullanarak dosyayı oluşturuyoruz ve dosya akışını açıyoruz. Daha sonra gelen dosyayı bu akışa kopyalıyoruz.
            await using var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write,FileShare.None, 1024 * 1024, useAsync: false);
            await file.CopyToAsync(fileStream);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}