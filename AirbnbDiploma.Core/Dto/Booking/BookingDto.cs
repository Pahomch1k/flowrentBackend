using AirbnbDiploma.Core.Enums;

namespace AirbnbDiploma.Core.Dto.Booking;

public class BookingDto
{
    public string? UserImageUrl { get; set; }

    public string UserName { get; set; }

    public string StayName { get; set; }

    public string StayLocation { get; set; }

    public DateTime CheckInDate { get; set; }

    public DateTime CheckOutDate { get; set; }

    public DateTime BookedDate { get; set; }

    public float Price { get; set; }

    public BookingStatus Status { get; set; }
}
