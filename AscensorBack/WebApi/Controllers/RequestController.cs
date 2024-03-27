using Business.Interfaces;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Utils.Responses;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController: ControllerBase
    {
        private IRequestService _requestService;

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpGet, Route("GetRequests")]
        public async Task<ActionResult> GetRequests()
        {
            PetitionResponse res = await _requestService.GetRequests();
            return Ok(res);
        }

        [HttpPost, Route("StopElevator")]
        public async Task<ActionResult> StopElevator()
        {
            PetitionResponse res = await _requestService.StopElevator();
            return Ok(res);
        }
    }
}
