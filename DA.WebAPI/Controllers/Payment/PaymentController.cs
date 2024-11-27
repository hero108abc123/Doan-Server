using Microsoft.AspNetCore.Mvc;
using DA.Payment.ApplicationService;
using DA.Payment.Dtos;

namespace DA.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly VNPayService _vnPayService;

        public PaymentController()
        {
            _vnPayService = new VNPayService();
        }

        [HttpPost("create-vnpayment")]
        public IActionResult CreateVNPayment([FromBody] VNPayRequestDto request)
        {
            if (request == null || request.Amount <= 0)
            {
                return BadRequest("Invalid payment request");
            }

            var paymentUrl = _vnPayService.CreatePaymentUrl(request.Amount, request.OrderInfo, request.ReturnUrl);
            return Ok(new { paymentUrl });
        }
    }
}
