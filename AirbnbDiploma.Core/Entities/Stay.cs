using AirbnbDiploma.Core.Entities.Base;
using AirbnbDiploma.Core.Enums;

namespace AirbnbDiploma.Core.Entities;

public class Stay : IEntity<int>
{
    public int Id { get; set; }

    public Guid OwnerId { get; set; }

    public int RegionId { get; set; }

    public string Title { get; set; }

    public StayStatus Status { get; set; } = StayStatus.Upcoming;

    public PlaceType PlaceType { get; set; }

    public PropertyType PropertyType { get; set; }

    public string Description { get; set; }

    public string CoverImageUrl { get; set; }

    public string Location { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int MaxGuests { get; set; }

    public int Beds { get; set; }

    public int Bedrooms { get; set; }

    public int Bathrooms { get; set; }

    public int Price { get; set; }

    public bool InstantBook { get; set; }

    public bool SelfCheckIn { get; set; }

    public bool AllowsPets { get; set; }

    public int CleaningFee { get; set; }

    public float OverallRating { get; set; }

    public float CleanlinessRating { get; set; }

    public float AccuracyRating { get; set; }

    public float CheckInRating { get; set; }

    public float CommunicationRating { get; set; }

    public float LocationRating { get; set; }

    public float ValueRating { get; set; }

    public User Owner { get; set; }

    public ICollection<Image> ImageUrls { get; set; } = new List<Image>();

    public ICollection<Tag> Amenities { get; set; } = new List<Tag>();

    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}