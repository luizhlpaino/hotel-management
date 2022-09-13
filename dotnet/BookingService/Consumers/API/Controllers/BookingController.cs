using Application.DTO.Booking;
using Application.Ports.Booking;
using Application.Requests.Booking;
using Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly ILogger<BookingController> _logger;
        private readonly IBookingManager _bookingManager;

        public BookingController(
            ILogger<BookingController> logger,
            IBookingManager bookingManager
        )
        {
            _logger = logger;
            _bookingManager = bookingManager;
        }

        [HttpPost]
        public async Task<ActionResult<BookingDTO>> Post(BookingDTO booking)
        {
            var request = new CreateBookingRequest
            {
                Data = booking
            };

            var response = await _bookingManager.CreateBooking(request);

            if (response.Success) return Created("", response.Data);

            switch (response.ErrorCode)
            {
                case ErrorCodes.BOOKING_NOT_FOUND: return NotFound(response);
                case ErrorCodes.BOOKING_MISSING_REQUIRED_INFORMATION: return BadRequest(response);
                case ErrorCodes.BOOKING_COULD_NOT_STORE_DATA: return BadRequest(response);
            }

            _logger.LogError("Responses with unknown ErrorCode returned", response.ErrorCode);
            return BadRequest(500);
        }

        [HttpGet]
        public async Task<ActionResult<BookingDTO>> Get(int bookingId)
        {
            var response = await _bookingManager.GetBooking(bookingId);

            if (response.Success) return Created("", response.Data);

            return NotFound(response);
        }
    }
}