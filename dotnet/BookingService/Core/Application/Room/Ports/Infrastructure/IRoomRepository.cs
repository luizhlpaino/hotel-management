using Entities = Domain.Entities;

namespace Application.Ports.Room
{
    public interface IRoomRepository
    {
        Task<Entities.Room> Get(int Id);
        Task<int> Create(Entities.Room room);
    }
}