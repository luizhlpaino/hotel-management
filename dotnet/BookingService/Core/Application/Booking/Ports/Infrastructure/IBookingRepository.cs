using Entities = Domain.Entities;
namespace Application.Ports.Booking
{
    public interface IBookingRepository
    {
        Task<Entities.Booking> Get(int Id);
        Task<int> Create(Entities.Booking booking);
    }
}