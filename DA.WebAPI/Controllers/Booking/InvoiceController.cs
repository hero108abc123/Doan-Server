using DA.Booking.ApplicationService.BookingModule.Abstracts;
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
    public class InvoiceController : ApiControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(ILogger<InvoiceController> logger, IInvoiceService invoiceService) : base(logger)
        {
            _invoiceService = invoiceService;
        }
        [AuthorizationFilter(UserTypes.Customer)]
        [HttpGet("get-invoice-by-customer")]
        public async Task<IActionResult> GetInvoicesByCustomer()
        {
            try
            {
                var invoices = await _invoiceService.GetInvoicesByCustomerAsync();
                return Ok(invoices);
            }
            catch (Exception ex)
            {
                return ReturnException(ex);
            }
        }
        [AuthorizationFilter(UserTypes.Admin)]
        [HttpGet("get-all-invoice")]
        public async Task<IActionResult> GetAllInvoices()
        {
            try
            {
                var invoices = await _invoiceService.GetAllInvoicesAsync();
                return Ok(invoices);
            }
            catch (Exception ex)
            {
                return ReturnException(ex);
            }
        }
    }

}
