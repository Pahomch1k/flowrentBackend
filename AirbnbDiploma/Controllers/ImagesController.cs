using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace AirbnbDiploma.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImagesController : ControllerBase
{
    [HttpGet("{imageName}")]
    public ActionResult GetFile(string imageName)
    {
        try
        {
            var currentDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            FileStream stream = new($"{currentDir}/images/{imageName}.jpg", FileMode.Open);
            FileStreamResult result = new(stream, "image/jpeg");
            return result;
        }
        catch (IOException)
        {
            return NotFound();
        }
    }
}
