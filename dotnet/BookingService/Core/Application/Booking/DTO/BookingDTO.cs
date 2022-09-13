using Application.DTO.Guest;
using Application.DTO.Room;
using Entities = Domain.Entities;
using Domain.Enums;

namespace Application.DTO.Booking
{
    public class BookingDTO
    {
        public BookingDTO()
        {
            this.StartDate = DateTime.UtcNow;
        }

        public int Id { get; set; }
        public DateTime PlacedAt { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int RoomId { get; set; }
        public int GuestId { get; set; }
        private Status Status { get; set; }

        public static Entities.Booking MapToEntity(BookingDTO bookingDTO)
        {
            return new Entities.Booking
            {
                Id = bookingDTO.Id,
                PlacedAt = bookingDTO.PlacedAt,
                StartDate = bookingDTO.StartDate,
                EndDate = bookingDTO.EndDate,
                Guest = new Entities.Guest { Id = bookingDTO.GuestId },
                Room = new Entities.Room { Id = bookingDTO.RoomId },
            };
        }

        public static BookingDTO MapToDto(Entities.Booking booking)
        {
            return new BookingDTO
            {
                Id = booking.Id,
                PlacedAt = booking.PlacedAt,
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,
                RoomId = booking.Room.Id,
                GuestId = booking.Guest.Id,
                Status = booking.Status,
            };
        }
    }
}