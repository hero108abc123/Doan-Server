using DA.Shared.Constant.Permission;
using DA.Vehicle.ApplicationService.BusModule.Abstracts;
using DA.Vehicle.Dtos.BusModule;
using DA.Vehicle.Dtos.SeatModule;
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
    public class BusController : ApiControllerBase
    {
        private readonly IBusService _busService;

        public BusController(ILogger<BusController> logger, IBusService busService) : base(logger)
        {
            _busService = busService;
        }

        [AuthorizationFilter(UserTypes.Admin)]
        [HttpPost("add-bus")]
        public async Task<IActionResult> AddBus([FromBody] CreateBusDto input)
        {
            try
            {
                await _busService.AddBusAsync(input);
                return Ok(new { message = "Bus created successfully." });
            }
            catch (Exception ex)
            {
                return ReturnException(ex);
            }
        }

        [AuthorizationFilter(UserTypes.Admin)]
        [HttpPut("update-bus")]
        public async Task<IActionResult> UpdateBus([FromBody] UpdateBusDto input)
        {
            try
            {
                await _busService.UpdateBusAsync(input);
                return Ok(new { message = "Bus updated successfully." });
            }
            catch (Exception ex)
            {
                return ReturnException(ex);
            }
        }

        [AuthorizationFilter(UserTypes.Admin)]
        [HttpDelete("delete-bus/{id}")]
        public async Task<IActionResult> DeleteBus(int id)
        {
            try
            {
                await _busService.DeleteBusAsync(id);
                return Ok(new { message = "Bus deleted successfully." });
            }
            catch (Exception ex)
            {
                return ReturnException(ex);
            }
        }

    }
}
