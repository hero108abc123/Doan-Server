using DA.Shared.Constant.Permission;
using DA.Vehicle.ApplicationService.BusModule.Abstracts;
using DA.Vehicle.Dtos.SeatModule;
using DA.WebAPI.Controllers.Base;
using DA.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DA.WebAPI.Controllers.Vehicle
{
    [Authorize]
    [AuthorizationFilter(UserTypes.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class SeatController : ApiControllerBase
    {
        private readonly ISeatService _seatService;

        public SeatController(ILogger<SeatController> logger, ISeatService seatService) : base(logger)
        {
            _seatService = seatService;
        }

        [HttpPost("add-seat")]
        public async Task<IActionResult> AddSeat([FromBody] CreateSeatDto input)
        {
            try
            {
                await _seatService.AddSeatAsync(input);
                return Ok(new { message = "Seat created successfully."});
            }
            catch (Exception ex)
            {
                return ReturnException(ex);
            }
        }

        [HttpPut("update-seat-position")]
        public async Task<IActionResult> UpdateSeatPosition([FromBody] UpdateSeatIPositionDto input)
        {
            try
            {
                await _seatService.UpdateSeatPositionAsync(input);
                return Ok(new { message = "Seat position updated successfully." });
            }
            catch (Exception ex)
            {
                return ReturnException(ex);
            }
        }

        [HttpPut("update-seat-status")]
        public async Task<IActionResult> UpdateSeatStatus([FromBody] UpdateSeatStatusDto input)
        {
            try
            {
                await _seatService.UpdateSeatStatusAsync(input);
                return Ok(new { message = "Seat status updated successfully." });
            }
            catch (Exception ex)
            {
                return ReturnException(ex);
            }
        }

        [HttpDelete("delete-seat/{seatId}")]
        public async Task<IActionResult> DeleteSeat(int seatId)
        {
            try
            {
                await _seatService.DeleteSeatAsync(seatId);
                return Ok(new { message = "Seat deleted successfully." });
            }
            catch (Exception ex)
            {
                return ReturnException(ex);
            }
        }
    }
}
