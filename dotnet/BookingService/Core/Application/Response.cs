namespace Application.Responses
{
    public enum ErrorCodes
    {
        //Guest error codes from 0 to 99
        GUEST_NOT_FOUND = 0,
        GUEST_COULD_NOT_STORE_DATA = 1,
        GUEST_INVALID_PERSON_ID = 2,
        GUEST_MISSING_REQUIRED_INFORMATION = 3,
        GUEST_INVALID_EMAIL = 4,

        //Room error codes from 100 to 199
        ROOM_NOT_FOUND = 100,
        ROOM_COULD_NOT_STORE_DATA = 101,
        ROOM_INVALID_PERSON_ID = 102,
        ROOM_MISSING_REQUIRED_INFORMATION = 102,
        ROOM_INVALID_EMAIL = 103,

        //Booking error codes from 200 to 299
        BOOKING_NOT_FOUND = 200,
        BOOKING_COULD_NOT_STORE_DATA = 201,
        BOOKING_INVALID_PERSON_ID = 202,
        BOOKING_MISSING_REQUIRED_INFORMATION = 202,
        BOOKING_INVALID_EMAIL = 203,
        BOOKING_ROOM_CANT_BE_BOOKED = 204,
    }

    public abstract class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ErrorCodes ErrorCode { get; set; }
    }
}