using DA.Shared.ApplicationService.Vehicle;
using DA.Shared.Constant.Permission;
using DA.Vehicle.Dtos.BusRideModule;
using DA.WebAPI.Controllers.Base;
using DA.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DA.WebAPI.Controllers.Vehicle
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BusRideController : ApiControllerBase
    {
        private readonly IBusRideService _busRideService;

        public BusRideController(ILogger<BusRideController> logger, IBusRideService busRideService) : base(logger)
        {
            _busRideService = busRideService;
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetBusRideById(int id)
        {
            try
            {
                var busRide = await _busRideService.GetBusRideByIdAsync(id);
                return Ok(busRide);
            }
            catch (Exception ex)
            {
                return ReturnException(ex);
            }
        }

        [AuthorizationFilter(UserTypes.Admin)]
        [HttpPost("create")]
        public async Task<IActionResult> CreateBusRide([FromBody] CreateBusRideDto input)
        {
            try
            {
                await _busRideService.CreateBusRideAsync(input);
                return Ok(new { message = "Bus ride created successfully." });
            }
            catch (Exception ex)
            {
                return ReturnException(ex);
            }
        }

        [AuthorizationFilter(UserTypes.Admin)]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateBusRide([FromBody] UpdateBusRideDto input)
        {
            try
            {
                await _busRideService.UpdateBusRideAsync(input);
                return Ok(new { message = "Bus ride updated successfully." });
            }
            catch (Exception ex)
            {
                return ReturnException(ex);
            }
        }

        [AuthorizationFilter(UserTypes.Admin)]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteBusRide(int id)
        {
            try
            {
                await _busRideService.DeleteBusRideAsync(id);
                return Ok(new { message = "Bus ride deleted successfully." });
            }
            catch (Exception ex)
            {
                return ReturnException(ex);
            }
        }

        [HttpGet("{busRideId}/get-seats")]
        public async Task<IActionResult> GetAllSeats(int busRideId)
        {
            try
            {
                var seats = await _busRideService.GetAllSeatsAsync(busRideId);
                return Ok(seats);
            }
            catch (Exception ex)
            {
                return ReturnException(ex);
            }
        }

        [HttpGet("{busRideId}/get-available-seats")]
        public async Task<IActionResult> GetAvailableSeats(int busRideId)
        {
            try
            {
                var availableSeats = await _busRideService.GetAvailableSeatsAsync(busRideId);
                return Ok(availableSeats);
            }
            catch (Exception ex)
            {
                return ReturnException(ex);
            }
        }
    }
}
