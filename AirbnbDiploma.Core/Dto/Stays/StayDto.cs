using AirbnbDiploma.Core.Dto.Users;

namespace AirbnbDiploma.Core.Dto.Stays;

public class StayDto
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string Location { get; set; }

    public int MaxGuests { get; set; }

    public int Beds { get; set; }

    public int Bedrooms { get; set; }

    public int Bathrooms { get; set; }

    public int Price { get; set; }

    public int CleaningFee { get; set; }

    public RatingDto Rating { get; set; }

    public UserInfoDto Owner { get; set; }

    public IEnumerable<string> ImageUrls { get; set; }

    public IEnumerable<TagTypeDto> Amenities { get; set; }
}
