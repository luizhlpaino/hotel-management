using Domain.Enums;
using Domain.Exceptions;
using Action = Domain.Enums.Action;

namespace Domain.Entities
{
    public class Booking
    {
        public Booking()
        {
            this.Status = Status.Created;
            this.PlacedAt = DateTime.UtcNow;
        }

        public int Id { get; set; }
        public DateTime PlacedAt { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Room Room { get; set; }
        public Guest Guest { get; set; }
        private Status Status { get; set; }
        public Status CurrentStatus { get { return this.Status; } }

        public void ChangeState(Action action)
        {
            this.Status = (this.Status, action) switch
            {
                (Status.Created, Action.Pay) => Status.Paid,
                (Status.Created, Action.Cancel) => Status.Canceled,
                (Status.Paid, Action.Finish) => Status.Finished,
                (Status.Paid, Action.Refound) => Status.Refounded,
                (Status.Canceled, Action.Reopen) => Status.Created,
                _ => this.Status
            };
        }

        public bool IsValid()
        {
            try
            {
                this.ValidateState();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void ValidateState()
        {
            if (this.PlacedAt == default(DateTime))
                throw new PlacedAtIsRequiredException();

            if (this.StartDate == default(DateTime))
                throw new StartDateIsRequiredException();

            if (this.EndDate == default(DateTime))
                throw new EndDateIsRequiredException();

            if (string.IsNullOrEmpty(Convert.ToString(this.Guest)))
                throw new GuestIsRequiredException();

            if (string.IsNullOrEmpty(Convert.ToString(this.Room)))
                throw new RoomIsRequiredException();

            if (!this.Room.CanBeBooked)
                throw new RoomCantBeBookedException();

            this.Guest.IsValid();
        }
    }
}