using AirbnbDiploma.BLL.Services.EmailService;
using AirbnbDiploma.BLL.Services.UserService;
using AirbnbDiploma.Core.Constants;
using AirbnbDiploma.Core.Dto.Booking;
using AirbnbDiploma.Core.EmailTemplates.Arguments;
using AirbnbDiploma.Core.Entities;
using AirbnbDiploma.Core.Enums;
using AirbnbDiploma.DAL.UnitOfWork;

namespace AirbnbDiploma.BLL.Services.BookingService;
public class BookingService : IBookingService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserService _userService;
    private readonly IEmailService _emailService;

    public BookingService(IUnitOfWork unitOfWork, IUserService userService, IEmailService emailService)
    {
        _unitOfWork = unitOfWork;
        _userService = userService;
        _emailService = emailService;
    }

    public async Task BookAsync(BookingRequestDto bookingRequest)
    {
        var user = await _userService.GetUserInfoAsync();
        var stay = await _unitOfWork.StaysRepository.GetByIdAsync(bookingRequest.StayId);
        Booking booking = new()
        {
            CheckInDate = bookingRequest.CheckInDate,
            CheckOutDate = bookingRequest.CheckOutDate,
            Guests = bookingRequest.Guests,
            Status = BookingStatus.AwaitingPayment,
            StayId = bookingRequest.StayId,
            UserId = user.Id
        };

        await _unitOfWork.BookingRepository.AddAsync(booking);
        await _unitOfWork.CommitAsync();

        BookingSuccessfulArguments arguments = new()
        {
            Name = stay.Title,
            Location = stay.Location,
            StartDate = booking.CheckInDate.ToString(),
            EndDate = booking.CheckOutDate.ToString(),
            Cost = ((booking.CheckOutDate - booking.CheckInDate).Days * stay.Price).ToString(),
        };
        await _emailService.SendAsync(user.Email, "Airbnb successful booking", HtmlTemplateNames.BookingSuccessful, arguments);
    }
}
