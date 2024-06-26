﻿using AirbnbDiploma.BLL.Services.BookingService;
using AirbnbDiploma.Core.Dto.Booking;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirbnbDiploma.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpPost]
    public async Task<ActionResult> BookAsync(BookingRequestDto bookingRequest)
    {
        await _bookingService.BookAsync(bookingRequest);
        return Created("", null);
    }

    [Authorize]
    [HttpGet("my")]
    public async Task<ActionResult<IEnumerable<BookingDto>>> GetBookingsOfMyStays()
    {
        var bookings = await _bookingService.GetMyStaysBookingsAsync();
        return Ok(bookings);
    }
}
