namespace AirbnbDiploma.Core.Dto.Booking;

public class BookingRequestDto
{
    public Guid StayId { get; set; }

    public DateTime CheckInDate { get; set; }

    public DateTime CheckOutDate { get; set; }

    public int Guests { get; set; }
}
