using Application.DTO.Guest;
using Application.Ports.Guest;
using Application.Requests.Guest;
using Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GuestController : ControllerBase
    {
        private readonly ILogger<GuestController> _logger;
        private readonly IGuestManager _guestManager;

        public GuestController(
            ILogger<GuestController> logger,
            IGuestManager guestManager
        )
        {
            _logger = logger;
            _guestManager = guestManager;
        }

        [HttpPost]
        public async Task<ActionResult<GuestDTO>> Post(GuestDTO guest)
        {
            var request = new CreateGuestRequest
            {
                Data = guest
            };

            var res = await _guestManager.CreateGuest(request);

            if (res.Success) return Created("", res.Data);

            switch (res.ErrorCode)
            {
                case ErrorCodes.GUEST_NOT_FOUND: return NotFound(res);
                case ErrorCodes.GUEST_INVALID_EMAIL: return BadRequest(res);
                case ErrorCodes.GUEST_MISSING_REQUIRED_INFORMATION: return BadRequest(res);
                case ErrorCodes.GUEST_INVALID_PERSON_ID: return BadRequest(res);
                case ErrorCodes.GUEST_COULD_NOT_STORE_DATA: return BadRequest(res);
            }

            _logger.LogError("Responses with unknown ErrorCode returned", res);
            return BadRequest(500);
        }

        [HttpGet]
        public async Task<ActionResult<GuestDTO>> Get(int guestId)
        {
            var response = await _guestManager.GetGuest(guestId);

            if (response.Success) return Created("", response.Data);

            return NotFound(response);
        }
    }
}