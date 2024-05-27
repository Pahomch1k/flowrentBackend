namespace AirbnbDiploma.BLL.Services.ImageService;

public interface IImageService
{
    public Task SaveImageAsync(string name, string contentBase64);
}
