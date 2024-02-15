using AirbnbDiploma.Core.Entities.Base;
using AirbnbDiploma.Core.Enums;

namespace AirbnbDiploma.Core.Entities;

public class Booking : IEntity<int>
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int StayId { get; set; }

    public DateTime CheckInDate { get; set; }

    public DateTime CheckOutDate { get; set; }

    public int Guests { get; set; }

    public BookingStatus Status { get; set; }
}
