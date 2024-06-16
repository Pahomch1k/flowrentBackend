using AirbnbDiploma.Core.Enums;

namespace AirbnbDiploma.Core.Dto.Stays;

public class NewStayDto
{
    public string Title { get; set; }

    public string Description { get; set; }

    public string Location { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int Bedrooms { get; set; }

    public int Beds { get; set; }

    public int Bathrooms { get; set; }

    public int MaxGuests { get; set; }

    public int Price { get; set; }

    public PlaceType PlaceType { get; set; }

    public IEnumerable<string> Images { get; set; }

    public IEnumerable<int> Places { get; set; }

    public IEnumerable<int> Offers { get; set; }

    public IEnumerable<int> Amenities { get; set; }

    public IEnumerable<int> Safetys { get; set; }
}
