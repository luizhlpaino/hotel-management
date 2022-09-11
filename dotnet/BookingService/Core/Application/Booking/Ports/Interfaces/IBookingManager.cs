using Application.Requests.Booking;
using Application.Responses.Booking;

namespace Application.Ports.Booking
{
    public interface IBookingManager
    {
        Task<BookingResponse> CreateBooking(CreateBookingRequest request);
        Task<BookingResponse> GetBooking(int bookingId);
    }
}