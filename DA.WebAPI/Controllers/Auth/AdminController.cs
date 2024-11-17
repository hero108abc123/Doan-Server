using DA.Auth.ApplicationService.UserModule.Abstracts;
using DA.Auth.ApplicationService.UserModule.Implements;
using DA.Auth.Dtos.CustomerModule;
using DA.Shared.Constant.Permission;
using DA.Shared.Dtos;
using DA.WebAPI.Controllers.Base;
using DA.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DA.WebAPI.Controllers.Auth
{
    [Authorize]
    [AuthorizationFilter(UserTypes.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ApiControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(ILogger<AdminController> logger, IAdminService adminService) : base(logger)
        {
            _adminService = adminService;
        }

        [HttpGet("get-all-customer")]
        public IActionResult GetAll([FromQuery] FilterDto input)
        {
            try
            {
                var customers = _adminService.GetAll(input);
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return ReturnException(ex);
            }
        }

        [HttpGet("get-customer")]
        public IActionResult GetCustomer([FromQuery] int id)
        {
            try
            {
                var customer = _adminService.GetCustomer(id);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return ReturnException(ex);

            }
        }


        [HttpPut("update-customer")]
        public IActionResult UpdateCustomer([FromBody] UpdateCustomerByAdminDto input)
        {
            try
            {
                _adminService.UpdateCustomer(input);
                return Ok();
            }
            catch (Exception ex)
            {
                return ReturnException(ex);

            }
        }


        [HttpDelete("delete-customer/{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            try
            {
                _adminService.DeleteCustomer(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return ReturnException(ex);

            }
        }
    }
}
