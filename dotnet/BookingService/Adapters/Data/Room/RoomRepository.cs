using Application.Ports.Room;
using Microsoft.EntityFrameworkCore;

namespace Data.Room
{
    public class RoomRepository : IRoomRepository
    {
        private HotelDbContext _hotelDbContext;
        public RoomRepository(HotelDbContext hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;
        }

        public async Task<int> Create(Domain.Entities.Room room)
        {
            _hotelDbContext.Rooms.Add(room);
            await _hotelDbContext.SaveChangesAsync();
            return room.Id;
        }

        public Task<Domain.Entities.Room> Get(int roomId)
        {
            return _hotelDbContext.Rooms.Where(r => r.Id == roomId).FirstOrDefaultAsync();
        }
    }
}