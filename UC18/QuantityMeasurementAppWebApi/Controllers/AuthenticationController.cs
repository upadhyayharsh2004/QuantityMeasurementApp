using System;
using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementAppModels.DTOs;
using QuantityMeasurementAppServices.Interfaces;

namespace QuantityMeasurementAppWebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService authService;

        // Constructor Injection
        public AuthenticationController(IAuthService authService)
        {
            this.authService = authService;
        }

        /// <summary>
        /// Create a new user account
        /// </summary>
        [HttpPost("signup")]
        [ProducesResponseType(typeof(AuthenticationResponse), 200)]
        [ProducesResponseType(400)]
        public IActionResult SignUp([FromBody] RegisterRequestService requestDTO)
        {
            if (requestDTO == null)
            {
                return BadRequest("Invalid request data");
            }

            var result = authService.SignUp(requestDTO);

            return Ok(result);
        }
        // [HttpPost("signup")]
        // public IActionResult SignUp(RegisterRequestService requestDTO)
        // {
        //     if (requestDTO == null)
        //     {
        //         return BadRequest("Invalid request data");
        //     }
        //     var result = authService.SignUp(requestDTO);
        //     return Ok(result);
        // }


        /// <summary>
        /// Authenticate user and return JWT token
        /// </summary>
        [HttpPost("signin")]
        [ProducesResponseType(typeof(AuthenticationResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult SignIn([FromBody] LoginRequestService loginDTO)
        {
            if (loginDTO == null)
            {
                return BadRequest("Invalid login data");
            }

            var result = authService.SignIn(loginDTO);

            return Ok(result);
        }

        /// <summary>
        /// Health check for authentication module
        /// </summary>
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok(new { Message = "Auth running. Register at POST /api/v1/auth/register, Login at POST /api/v1/auth/login" });
        }
    }
}