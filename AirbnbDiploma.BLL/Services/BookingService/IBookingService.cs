using AirbnbDiploma.Core.Dto.Booking;

namespace AirbnbDiploma.BLL.Services.BookingService;

public interface IBookingService
{
    public Task BookAsync(BookingRequestDto bookingRequest);

    Task<IEnumerable<BookingDto>> GetMyStaysBookingsAsync();
}
