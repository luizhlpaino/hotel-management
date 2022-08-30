using NUnit.Framework;
using Domain.Entities;
using Domain.Enums;
using Action = Domain.Enums.Action;

namespace DomainTests.Bookings
{
    public class StateMachine
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Should_Always_Start_With_Create_Status()
        {
            var booking = new Booking();
            Assert.That(booking.CurrentStatus, Is.EqualTo(Status.Created));
        }

        [Test]
        public void Should_Set_Status_To_Paid_When_Paying_For_A_Booking_With_Created_Status()
        {
            var booking = new Booking();
            booking.ChangeState(Action.Pay);
            Assert.That(booking.CurrentStatus, Is.EqualTo(Status.Paid));
        }

        [Test]
        public void Should_Set_Status_To_Finished_After_Finishing_A_Booking_With_Paid_Status()
        {
            var booking = new Booking();
            booking.ChangeState(Action.Pay);
            booking.ChangeState(Action.Finish);
            Assert.That(booking.CurrentStatus, Is.EqualTo(Status.Finished));
        }

        [Test]
        public void Should_Set_Status_To_Canceled_When_Canceling_For_A_Booking_With_Created_Status()
        {
            var booking = new Booking();
            booking.ChangeState(Action.Cancel);
            Assert.That(booking.CurrentStatus, Is.EqualTo(Status.Canceled));
        }

        [Test]
        public void Should_Set_Status_To_Created_When_Reopening_A_Canceled_Booking()
        {
            var booking = new Booking();
            booking.ChangeState(Action.Cancel);
            booking.ChangeState(Action.Reopen);
            Assert.That(booking.CurrentStatus, Is.EqualTo(Status.Created));
        }

        [Test]
        public void Should_Set_Status_To_Refounded_When_Refounding_A_Paid_Booking()
        {
            var booking = new Booking();
            booking.ChangeState(Action.Pay);
            booking.ChangeState(Action.Refound);
            Assert.That(booking.CurrentStatus, Is.EqualTo(Status.Refounded));
        }
    }
}