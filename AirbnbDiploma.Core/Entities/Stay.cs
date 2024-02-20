using AirbnbDiploma.Core.Entities.Base;

namespace AirbnbDiploma.Core.Entities;

public class Stay : IEntity<int>
{

    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string Location { get; set; }

    public int MaxGuests { get; set; }

    public int Price { get; set; }

    public int CleaningFee { get; set; }

    public float OverallRating { get; set; }

    public byte CleanlinessRating { get; set; }

    public byte AccuracyRating { get; set; }

    public byte CheckInRating { get; set; }

    public byte CommunicationRating { get; set; }

    public byte LocationRating { get; set; }

    public byte ValueRating { get; set; }

    public virtual ICollection<Image> ImageUrls { get; set; }

    public virtual ICollection<Tag> Amenities { get; set; }
}