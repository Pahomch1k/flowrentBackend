using AirbnbDiploma.Core.Enums;

namespace AirbnbDiploma.Core.Dto.Stays;

public class StayBriefDto
{
    public int Id { get; set; }

    public string ImageUrl { get; set; }

    public string Name { get; set; }

    public string Place { get; set; }

    public StayStatus Status { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public float Rating { get; set; }

    public int Price { get; set; }
}
