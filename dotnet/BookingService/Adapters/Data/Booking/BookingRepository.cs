using Application.Ports.Booking;
using Microsoft.EntityFrameworkCore;

namespace Data.Booking
{
    public class BookingRepository : IBookingRepository
    {
        private readonly HotelDbContext _hotelDbContext;

        public BookingRepository(HotelDbContext hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;
        }

        public async Task<int> Create(Domain.Entities.Booking booking)
        {
            _hotelDbContext.Bookings.Add(booking);
            await _hotelDbContext.SaveChangesAsync();
            return booking.Id;
        }

        public Task<Domain.Entities.Booking> Get(int Id)
        {
            return _hotelDbContext.Bookings.Where(x => x.Id == Id).FirstAsync();
        }
    }
}