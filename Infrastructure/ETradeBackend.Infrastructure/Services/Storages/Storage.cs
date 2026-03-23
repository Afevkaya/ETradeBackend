using ETradeBackend.Infrastructure.Operations;

namespace ETradeBackend.Infrastructure.Services.Storages;

public class Storage
{
    protected delegate bool HasFile(string pathOrContainerName, string fileName);
    protected async Task<string> FileRenameAsync(string pathOrContainerName, string fileName, HasFile hasFileMethod)
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
            while (hasFileMethod(pathOrContainerName,newFileName)) 
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
}