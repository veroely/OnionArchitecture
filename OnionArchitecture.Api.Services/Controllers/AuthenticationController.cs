using Microsoft.AspNetCore.Mvc;
using OnionArchitecture.Core.Dtos.Requests;
using OnionArchitecture.Core.Dtos.Responses;
using OnionArchitecture.Core.Services.Contracts;
using System;
using System.Threading.Tasks;

namespace OnionArchitecture.Api.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(UserRegisterRequestDto user) {
            await _authenticationService.Register(user);
            return Ok();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<UserLoginrResponseDto> Login(UserLoginRequestDto user)
        {
            try
            {
                return await _authenticationService.Login(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
