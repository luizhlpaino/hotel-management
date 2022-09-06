using Application.DTO.Room;
using Application.Ports.Room;
using Application.Requests.Room;
using Application.Responses;
using Application.Responses.Room;

namespace Application.Room
{
    public class RoomManager : IRoomManager
    {
        private IRoomRepository _roomRepository;
        public RoomManager(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<RoomResponse> CreateRoom(CreateRoomRequest request)
        {
            try
            {
                var room = RoomDTO.MapToEntity(request.Data);

                room.ValidateState();

                if (room.Id == 0)
                    room.Id = await _roomRepository.Create(room);

                request.Data.Id = room.Id;

                return new RoomResponse
                {
                    Success = true,
                    Data = request.Data,
                };
            }
            catch (Exception)
            {
                return new RoomResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.COULD_NOT_STORE_DATA,
                    Message = "There was an error creating the room."
                };
            }
        }

        public async Task<RoomResponse> GetRoom(int roomId)
        {
            var room = await _roomRepository.Get(roomId);

            if (room == null)
            {
                return new RoomResponse
                {
                    Success = false,
                    Message = "The room does not exist",
                    ErrorCode = ErrorCodes.ROOM_NOT_FOUND,
                };
            }
            else
            {
                return new RoomResponse
                {
                    Success = true,
                    Data = RoomDTO.MapToDto(room),
                };
            }
        }
    }
}