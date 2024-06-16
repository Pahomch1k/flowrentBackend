using AirbnbDiploma.Core.Entities.Base;
using AirbnbDiploma.Core.Enums;

namespace AirbnbDiploma.Core.Entities;

public class Booking : IEntity<int>
{
    public int Id { get; set; }

    public Guid UserId { get; set; }

    public Guid StayId { get; set; }

    public DateTime CheckInDate { get; set; }

    public DateTime CheckOutDate { get; set; }

    public DateTime BookedDate { get; set; } = DateTime.UtcNow;

    public int Guests { get; set; }

    public BookingStatus Status { get; set; }

    public Stay Stay { get; set; }

    public User User { get; set; }
}
