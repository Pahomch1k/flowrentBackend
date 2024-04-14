using AirbnbDiploma.Core.Enums;

namespace AirbnbDiploma.Core.Dto.Stays;

public class TagDto
{
    public string Name { get; set; }

    public TagCategory Category { get; set; }
}
