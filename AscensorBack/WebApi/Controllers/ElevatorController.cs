using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Utils.Responses;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElevatorController : ControllerBase
    {
        private IElevatorService _elevatorService;

        public ElevatorController(IElevatorService elevatorService)
        {
            _elevatorService = elevatorService;
        }

        [HttpGet, Route("GetCurrentFloor") ]
        public async Task<ActionResult> GetElevator()
        {
            PetitionResponse res = await _elevatorService.GetElevator();
            return Ok(res);
        }

        [HttpPost, Route("AddRequestToGo")]
        public async Task<ActionResult> AddRequestToGo(int floor)
        {
            PetitionResponse res = await _elevatorService.AddRequestToGo(floor);
            return Ok(res);
        }
    }
}
