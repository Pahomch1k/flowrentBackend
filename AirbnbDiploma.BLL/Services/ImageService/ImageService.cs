using System.Reflection;

namespace AirbnbDiploma.BLL.Services.ImageService;

public class ImageService : IImageService
{
    public async Task SaveImageAsync(string name, string contentBase64)
    {
        var currentDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        Directory.CreateDirectory($"{currentDir}/images");
        var bytes = Convert.FromBase64String(contentBase64);
        await File.WriteAllBytesAsync($"{currentDir}/images/{name}.jpg", bytes);
    }
}
