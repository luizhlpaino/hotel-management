using Application.DTO.Guest;
using Application.Requests.Guest;
using Application.Responses.Guest;
using Application.Ports.Guest;
using Application.Responses;
using Domain.Exceptions;

namespace Application.Guest
{
    public class GuestManager : IGuestManager
    {
        private IGuestRepository _guestRepository;
        public GuestManager(IGuestRepository guestRepository)
        {
            _guestRepository = guestRepository;
        }

        public async Task<GuestResponse> CreateGuest(CreateGuestRequest request)
        {
            try
            {
                var guest = GuestDTO.MapToEntity(request.Data);

                guest.ValidateState();

                if (guest.Id == 0)
                    guest.Id = await _guestRepository.Create(guest);

                request.Data.Id = guest.Id;

                return new GuestResponse
                {
                    Success = true,
                    Data = request.Data,
                };
            }
            catch (InvalidPersonDocumentException e)
            {
                return new GuestResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.GUEST_INVALID_PERSON_ID,
                    Message = "The ID of the guest is invalid."
                };
            }
            catch (MissingRequiredInformationException e)
            {
                return new GuestResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.GUEST_MISSING_REQUIRED_INFORMATION,
                    Message = "Missing required information for the guest."
                };
            }
            catch (InvalidEmailException e)
            {
                return new GuestResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.GUEST_INVALID_EMAIL,
                    Message = "The email is invalid."
                };
            }
            catch (Exception)
            {
                return new GuestResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.GUEST_COULD_NOT_STORE_DATA,
                    Message = "There was an error creating the guest.",
                };
            }
        }

        public async Task<GuestResponse> GetGuest(int guestId)
        {
            var guest = await _guestRepository.Get(guestId);

            if (guest == null)
            {
                return new GuestResponse
                {
                    Success = false,
                    Message = "The guest does not exist.",
                    ErrorCode = ErrorCodes.GUEST_NOT_FOUND,
                };
            }
            else
            {
                return new GuestResponse
                {
                    Success = true,
                    Data = GuestDTO.MapToDto(guest),
                };
            }
        }
    }
}