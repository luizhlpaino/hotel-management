using Entities = Domain.Entities;

namespace Application.Ports.Room
{
    public interface IRoomRepository
    {
        Task<Entities.Room> Get(int id);
        Task<Entities.Room> GetAggregate(int id);
        Task<int> Create(Entities.Room room);
    }
}