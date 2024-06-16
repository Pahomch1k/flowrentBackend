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

    public int Guests { get; set; }

    public BookingStatus Status { get; set; }
}
