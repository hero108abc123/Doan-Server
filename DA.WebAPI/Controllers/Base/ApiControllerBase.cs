﻿using DA.Shared.Exceptions;
using DA.WebAPI.Body;
using Microsoft.AspNetCore.Mvc;

namespace DA.WebAPI.Controllers.Base
{
    public class ApiControllerBase : ControllerBase
    {
        protected ILogger _logger;
        public ApiControllerBase(ILogger logger)
        {
            _logger = logger;
        }

        protected IActionResult ReturnException(Exception ex)
        {
            if (ex is UserFriendlyException)
            {
                var userEx = ex as UserFriendlyException;
                return StatusCode(StatusCodes.Status400BadRequest, new ExceptionBody
                {
                    Message = userEx.Message
                });
            }
            _logger.LogError(ex, ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionBody
            {
                Message = ex.Message,
            });
        }
    }
}
