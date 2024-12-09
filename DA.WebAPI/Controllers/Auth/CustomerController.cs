using DA.Auth.Dtos.CustomerModule;
using DA.Shared.ApplicationService.Auth;
using DA.Shared.Constant.Permission;
using DA.WebAPI.Controllers.Base;
using DA.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DA.WebAPI.Controllers.Auth
{
    [Authorize]
    [AuthorizationFilter(UserTypes.Customer)]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ApiControllerBase

    {
        private readonly ICustomerService _customerService;
        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService) : base(logger)
        {
            _customerService = customerService;
        }

        [HttpGet("get-current-customer")]
        public IActionResult GetCustomer()
        {
            try
            {
                var customer = _customerService.GetCustomer();
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return ReturnException(ex);

            }
        }


        [HttpPost("create")]
        public IActionResult CreateCustomer(CreateCustomerDto input)
        {
            try
            {
                _customerService.CreateCustomer(input);
                return Ok();
            }
            catch (Exception ex)
            {
                return ReturnException(ex);

            }
        }


        [HttpPut("update")]
        public IActionResult UpdateCustomer(UpdateCustomerDto input)
        {
            try
            {
                _customerService.UpdateCustomer(input);
                return Ok(new { message = "Customer created successfully." });
            }
            catch (Exception ex)
            {
                return ReturnException(ex);

            }
        }


        [HttpDelete("delete")]
        public IActionResult DeleteCustomer()
        {
            try
            {
                _customerService.DeleteCustomer();
                return Ok(new { message = "Customer deleted successfully." });
            }
            catch (Exception ex)
            {
                return ReturnException(ex);

            }
        }

    }
}
