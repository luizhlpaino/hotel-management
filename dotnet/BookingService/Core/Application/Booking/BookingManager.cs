using Application.DTO.Booking;
using Application.Ports.Booking;
using Application.Ports.Guest;
using Application.Ports.Room;
using Application.Requests.Booking;
using Application.Responses;
using Application.Responses.Booking;
using Domain.Exceptions;

namespace Application.Booking
{
    public class BookingManager : IBookingManager
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IGuestRepository _guestRepository;
        private readonly IRoomRepository _roomRepository;

        public BookingManager(
            IBookingRepository bookingRepository,
            IGuestRepository guestRepository,
            IRoomRepository roomRepository
        )
        {
            _bookingRepository = bookingRepository;
            _guestRepository = guestRepository;
            _roomRepository = roomRepository;
        }

        public async Task<BookingResponse> CreateBooking(CreateBookingRequest request)
        {
            try
            {
                var booking = BookingDTO.MapToEntity(request.Data);
                booking.Guest = await _guestRepository.Get(request.Data.GuestId);
                booking.Room = await _roomRepository.GetAggregate(request.Data.RoomId);

                booking.ValidateState();

                if (booking.Id == 0)
                    booking.Id = await _bookingRepository.Create(booking);

                request.Data.Id = booking.Id;

                return new BookingResponse
                {
                    Success = true,
                    Data = request.Data,
                };
            }
            catch (PlacedAtIsRequiredException)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.BOOKING_MISSING_REQUIRED_INFORMATION,
                    Message = "PlacedAt is required for bookings to be created."
                };
            }
            catch (StartDateIsRequiredException)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.BOOKING_MISSING_REQUIRED_INFORMATION,
                    Message = "StartDate is required for bookings to be created."
                };
            }
            catch (EndDateIsRequiredException)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.BOOKING_MISSING_REQUIRED_INFORMATION,
                    Message = "EndDate is required for bookings to be created."
                };
            }
            catch (GuestIsRequiredException)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.BOOKING_MISSING_REQUIRED_INFORMATION,
                    Message = "Guest is required for bookings to be created."
                };
            }
            catch (RoomIsRequiredException)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.BOOKING_MISSING_REQUIRED_INFORMATION,
                    Message = "Room is required for bookings to be created."
                };
            }
            catch (RoomCantBeBookedException)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.BOOKING_ROOM_CANT_BE_BOOKED,
                    Message = "Room can't be booked."
                };
            }
            catch (Exception)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.BOOKING_COULD_NOT_STORE_DATA,
                    Message = "Booking could not be stored successfully."
                };
            }
        }


        public async Task<BookingResponse> GetBooking(int bookingId)
        {
            var booking = await _bookingRepository.Get(bookingId);

            if (booking == null)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.BOOKING_NOT_FOUND,
                    Message = "Booking not found for booking id: " + bookingId
                };
            }
            else
            {
                return new BookingResponse
                {
                    Success = true,
                    Data = BookingDTO.MapToDto(booking)
                };
            }
        }
    }

}