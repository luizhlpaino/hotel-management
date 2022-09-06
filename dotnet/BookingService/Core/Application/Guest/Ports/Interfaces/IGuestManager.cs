using Application.Requests.Guest;
using Application.Responses.Guest;

namespace Application.Ports.Guest
{
    public interface IGuestManager
    {
        Task<GuestResponse> CreateGuest(CreateGuestRequest request);
        Task<GuestResponse> GetGuest(int guestId);
    }
}