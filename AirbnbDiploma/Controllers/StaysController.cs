using AirbnbDiploma.Core.Dto.Stays;
using Microsoft.AspNetCore.Mvc;

namespace AirbnbDiploma.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StaysController : ControllerBase
{
    [HttpGet]
    public ActionResult<StayBriefDto> GetStays()
    {
        var stays = new List<StayBriefDto>();
        for (int i = 0; i < 20; i++)
        {
            stays.Add(new StayBriefDto
            {
                Id = 1,
                ImageUrl = "https://static.leonardo-hotels.com/image/leonardohotelbucharestcitycenter_room_comfortdouble2_2022_4000x2600_7e18f254bc75491965d36cc312e8111f_1200x780_mobile_3.jpeg",
                Name = "Grovland, California",
                Place = "Yosemite National Park",
                StartDate = new DateOnly(2024, 10, 23).ToDateTime(TimeOnly.MinValue),
                EndDate = new DateOnly(2024, 10, 28).ToDateTime(TimeOnly.MinValue),
                Rating = 4.91F,
                Price = 289,
            });
        }
        return Ok(stays);
    }
}