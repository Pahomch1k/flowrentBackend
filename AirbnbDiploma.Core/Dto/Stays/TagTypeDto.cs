namespace AirbnbDiploma.Core.Dto.Stays;

public class TagTypeDto
{
    public string Name { get; set; }

    public IEnumerable<TagDto> Tags { get; set; }
}
