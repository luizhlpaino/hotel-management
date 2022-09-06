using Application.DTO.Room;
using Application.Ports.Room;
using Application.Requests.Room;
using Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly ILogger<RoomController> _logger;
        private readonly IRoomManager _roomManager;

        public RoomController(
            ILogger<RoomController> logger,
            IRoomManager roomManager
        )
        {
            _logger = logger;
            _roomManager = roomManager;
        }

        [HttpPost]
        public async Task<ActionResult<RoomDTO>> Post(RoomDTO roomDTO)
        {
            var request = new CreateRoomRequest
            {
                Data = roomDTO
            };

            var response = await _roomManager.CreateRoom(request);

            if (response.Success) return Created("", response.Data);

            switch (response.ErrorCode)
            {
                case ErrorCodes.NOT_FOUND: return NotFound(response);
                case ErrorCodes.MISSING_REQUIRED_INFORMATION: return BadRequest(response);
                case ErrorCodes.COULD_NOT_STORE_DATA: return BadRequest(response);
            }

            _logger.LogError("Responses with unknown ErrorCode returned", response);
            return BadRequest(500);
        }

        [HttpGet]
        public async Task<ActionResult<RoomDTO>> Get(int roomId)
        {
            var response = await _roomManager.GetRoom(roomId);

            if (response.Success) return Created("", response.Data);

            return NotFound();
        }
    }
}