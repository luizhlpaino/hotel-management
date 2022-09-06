using Entities = Domain.Entities;

namespace Application.DTO.Room
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool InMaintenance { get; set; }
        public bool HasGuest { get; set; }

        public static Entities.Room MapToEntity(RoomDTO guestDTO)
        {
            return new Entities.Room
            {
                Id = guestDTO.Id,
                Name = guestDTO.Name,
                Level = guestDTO.Level,
                InMaintenance = guestDTO.InMaintenance,
            };
        }

        public static RoomDTO MapToDto(Entities.Room room)
        {
            return new RoomDTO
            {
                Id = room.Id,
                Name = room.Name,
                Level = room.Level,
                InMaintenance = room.InMaintenance,
            };
        }
    }
}