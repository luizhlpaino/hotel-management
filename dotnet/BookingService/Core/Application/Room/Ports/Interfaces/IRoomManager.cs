using Application.Requests.Room;
using Application.Responses.Room;

namespace Application.Ports.Room
{
    public interface IRoomManager
    {
        Task<RoomResponse> CreateRoom(CreateRoomRequest request);
        Task<RoomResponse> GetRoom(int roomId);
    }
}