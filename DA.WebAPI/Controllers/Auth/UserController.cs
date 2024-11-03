using DA.Auth.ApplicationService.UserModule.Abstracts;
using DA.Auth.Dtos.UserModule;
using DA.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DA.WebAPI.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiControllerBase
    {
        private readonly IUserService _authService;
        public UserController(
            IUserService authService,
            ILogger<UserController> logger) : base(logger)
        {
            _authService = authService;
        }

        [HttpPost("create")]
        public IActionResult CreateUser(CreateUserDto input)
        {
            try
            {
                _authService.CreateUser(input);
                return Ok();
            }
            catch (Exception ex)
            {
                return ReturnException(ex);
            }
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto input)
        {
            try
            {
                string token = _authService.Login(input);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return ReturnException(ex);
            }
        }
    }
}
