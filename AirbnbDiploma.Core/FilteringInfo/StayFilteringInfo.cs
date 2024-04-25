using AirbnbDiploma.Core.Enums;

namespace AirbnbDiploma.Core.FilteringInfo;

public class StayFilteringInfo
{
    public int Skip { get; set; }

    public int Count { get; set; }

    public PlaceType? PlaceType { get; set; }

    public int? RegionId { get; set; }

    public DateTime? CheckInDate { get; set; }

    public DateTime? CheckoutDate { get; set; }

    public short MinGuests { get; set; }

    public int MinPrice { get; set; }

    public int MaxPrice { get; set; }

    public short MinBedrooms { get; set; }

    public short MinBeds { get; set; }

    public short MinBathrooms { get; set; }

    public bool? TopTierStays { get; set; }

    public bool? InstantBook { get; set; }

    public bool? SelfCheckIn { get; set; }

    public bool? AllowsPets { get; set; }

    public IEnumerable<PropertyType>? PropertyTypes { get; set; }

    public IEnumerable<int>? Amenities { get; set; }

    public IEnumerable<int>? AccessibilityFeatures { get; set; }

    public IEnumerable<string>? Languages { get; set; }
}
