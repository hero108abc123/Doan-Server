using DA.Booking.ApplicationService.BookingModule.Abstracts;
using DA.Booking.Dtos.TicketModule;
using DA.Shared.Constant.Permission;
using DA.WebAPI.Controllers.Base;
using DA.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DA.WebAPI.Controllers.Booking
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ApiControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ILogger<TicketController> logger, ITicketService ticketService) : base(logger)
        {
            _ticketService = ticketService;
        }

        [HttpGet("get-by-id/{ticketId}")]
        public async Task<IActionResult> GetTicketById(int ticketId)
        {
            try
            {
                var ticket = await _ticketService.GetTicketByIdAsync(ticketId);
                return Ok(ticket);
            }
            catch (Exception ex)
            {
                return ReturnException(ex);
            }
        }

        [HttpGet("get-by-status/{status}")]
        public async Task<IActionResult> GetTicketsByStatus(int status)
        {
            try
            {
                var tickets = await _ticketService.GetTicketsByStatusAsync(status);
                return Ok(tickets);
            }
            catch (Exception ex)
            {
                return ReturnException(ex);
            }
        }
        [AuthorizationFilter(UserTypes.Customer)]
        [HttpGet("get-by-customer")]
        public async Task<IActionResult> GetTicketsByCustomer()
        {
            try
            {
                var tickets = await _ticketService.GetTicketsByCustomerAsync();
                return Ok(tickets);
            }
            catch (Exception ex)
            {
                return ReturnException(ex);
            }
        }

        [HttpGet("get-by-bus/{busRideId}")]
        public async Task<IActionResult> GetTicketsByBus(int busRideId)
        {
            try
            {
                var tickets = await _ticketService.GetTicketsByBusAsync(busRideId);
                return Ok(tickets);
            }
            catch (Exception ex)
            {
                return ReturnException(ex);
            }
        }

        [AuthorizationFilter(UserTypes.Admin)]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateTicketStatus([FromBody] UpdateTicketStatusDto input)
        {
            try
            {
                await _ticketService.UpdateTicketStatusAsync(input);
                return Ok(new { message = "Ticket status updated successfully." });
            }
            catch (Exception ex)
            {
                return ReturnException(ex);
            }
        }

        [AuthorizationFilter(UserTypes.Admin)]
        [HttpDelete("delete/{ticketId}")]
        public async Task<IActionResult> DeleteTicket(int ticketId)
        {
            try
            {
                await _ticketService.DeleteTicketAsync(ticketId);
                return Ok(new { message = "Ticket deleted successfully." });
            }
            catch (Exception ex)
            {
                return ReturnException(ex);
            }
        }
    }
}
