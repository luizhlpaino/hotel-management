using Domain.Enums;
using Entities = Domain.Entities;

namespace Application.DTO.Room
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool InMaintenance { get; set; }
        public decimal Price { get; set; }
        public AcceptedCurrencies Currency { get; set; }

        public static Entities.Room MapToEntity(RoomDTO roomDTO)
        {
            return new Entities.Room
            {
                Id = roomDTO.Id,
                Name = roomDTO.Name,
                Level = roomDTO.Level,
                InMaintenance = roomDTO.InMaintenance,
                Price = new Domain.ValueObjects.Price
                {
                    Currency = roomDTO.Currency,
                    Value = roomDTO.Price,
                },
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
                Price = room.Price.Value,
                Currency = room.Price.Currency,
            };
        }
    }
}