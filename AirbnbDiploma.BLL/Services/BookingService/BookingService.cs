using AirbnbDiploma.BLL.Services.UserService;
using AirbnbDiploma.Core.Dto.Booking;
using AirbnbDiploma.Core.Entities;
using AirbnbDiploma.Core.Enums;
using AirbnbDiploma.DAL.UnitOfWork;

namespace AirbnbDiploma.BLL.Services.BookingService;
public class BookingService : IBookingService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserService _userService;

    public BookingService(IUnitOfWork unitOfWork, IUserService userService)
    {
        _unitOfWork = unitOfWork;
        _userService = userService;
    }

    public async Task BookAsync(BookingRequestDto bookingRequest)
    {
        Booking booking = new()
        {
            CheckInDate = bookingRequest.CheckInDate,
            CheckOutDate = bookingRequest.CheckOutDate,
            Guests = bookingRequest.Guests,
            Status = BookingStatus.AwaitingPayment,
            StayId = bookingRequest.StayId,
            UserId = _userService.GetUserId()
        };

        await _unitOfWork.BookingRepository.AddAsync(booking);
        await _unitOfWork.CommitAsync();
    }
}
