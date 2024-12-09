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
    [AuthorizationFilter(UserTypes.Customer)]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ApiControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(ILogger<OrderController> logger, IOrderService orderService) : base(logger)
        {
            _orderService = orderService;
        }

        [HttpPost("booking")]
        public async Task<IActionResult> CreateTicket([FromBody] CreateTicketDto request)
        {
            try
            {
                await _orderService.CreateTicketAsync(request);
                return Ok(new { message = "Ticket created successfully." });
            }
            catch (Exception ex)
            {
                return ReturnException(ex);
            }
        }
    }
}
