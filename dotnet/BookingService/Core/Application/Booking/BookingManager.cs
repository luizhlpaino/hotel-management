using Application.DTO.Booking;
using Application.Ports.Booking;
using Application.Requests.Booking;
using Application.Responses.Booking;

namespace Application.Booking
{
    public class BookingManager : IBookingManager
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingManager(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<BookingResponse> CreateBooking(CreateBookingRequest request)
        {
            var booking = BookingDTO.MapToEntity(request.Data);
            booking.ValidateState();

            if (booking.Id == 0)
                booking.Id = await _bookingRepository.Create(booking);

            request.Data.Id = booking.Id;

            return new BookingResponse
            {
                Success = true,
                Data = request.Data,
            };
        }

        public Task<BookingResponse> GetBooking(int bookingId)
        {
            throw new NotImplementedException();
        }
    }

}