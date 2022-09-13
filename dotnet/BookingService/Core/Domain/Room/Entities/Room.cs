using Domain.Exceptions;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool InMaintenance { get; set; }
        public Price Price { get; set; }
        public ICollection<Booking> Bookings { get; set; }

        public bool HasGuest
        {
            get
            {
                var notAvaibleStatuses = new List<Enums.Status>()
                {
                    Enums.Status.Created,
                    Enums.Status.Paid
                };

                return this.Bookings.Where(
                    b => b.Room.Id == this.Id &&
                    notAvaibleStatuses.Contains(b.Status)
                ).Count() > 0;
            }
        }

        public bool CanBeBooked
        {
            get
            {
                try
                {
                    this.ValidateState();
                }
                catch (Exception)
                {
                    return false;
                }

                if (!this.IsAvailable)
                    return false;

                return true;
            }
        }

        public bool IsAvailable
        {
            get
            {
                return (this.InMaintenance || this.HasGuest) ? false : true;
            }
        }

        public void ValidateState()
        {
            if (string.IsNullOrEmpty(this.Name))
                throw new InvalidRoomDataException();

            if (this.Price == null || this.Price.Value < 10)
                throw new InvalidRoomDataException();
        }
    }
}