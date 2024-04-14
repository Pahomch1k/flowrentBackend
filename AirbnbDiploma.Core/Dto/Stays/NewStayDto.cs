namespace AirbnbDiploma.Core.Dto.Stays;

public class NewStayDto
{
    public string Title { get; set; }

    public string Description { get; set; }

    public string CoverImageUrl { get; set; }

    public string Location { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int MaxGuests { get; set; }

    public int Price { get; set; }

    public int CleaningFee { get; set; }

    public IEnumerable<string> ImageUrls { get; set; }

    public IEnumerable<TagTypeDto> Amenities { get; set; }
}
