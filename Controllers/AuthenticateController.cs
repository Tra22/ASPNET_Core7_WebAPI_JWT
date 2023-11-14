using Asp.Versioning;
using ASPNET_Core7_WebAPI_JWT.Dtos.Authentication;
using ASPNET_Core7_WebAPI_JWT.Payload.Global;
using ASPNET_Core7_WebAPI_JWT.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET_Core7_WebAPI_JWT.Controllers {
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthenticateController : ControllerBase {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticateController(IAuthenticationService authenticationService){
            this._authenticationService = authenticationService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticationRequestDto request){
            return Ok(new Response<AuthenticationResponseDto> {
                Message = "Successfully login.",
                Data = await _authenticationService.Authenticate(request)
            });
        }
        [HttpPost("register-user")]
        public async Task<IActionResult> RegisterAsUser([FromBody] RegisterDto request){
            return Ok(new Response<AuthenticationResponseDto> {
                Message = "Successfully registered user.",
                Data = await _authenticationService.RegisterAsUser(request)
            });
        }
        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAsAdmin([FromBody] RegisterDto request){
            return Ok(new Response<AuthenticationResponseDto> {
                Message = "Successfully registered admin.",
                Data = await _authenticationService.RegisterAsAdmin(request)
            });
        }
        [HttpPost("register-policy-useronly")]
        public async Task<IActionResult> RegisterWithPolicyUserOnly([FromBody] RegisterDto request){
            return Ok(new Response<AuthenticationResponseDto> {
                Message = "Successfully registered admin.",
                Data = await _authenticationService.RegisterWithPolicyUserOnly(request)
            });
        }
        [HttpPost("register-policy-userspecific")]
        public async Task<IActionResult> RegisterWithPolicyUserSpecificID([FromBody] RegisterDto request){
            return Ok(new Response<AuthenticationResponseDto> {
                Message = "Successfully registered admin.",
                Data = await _authenticationService.RegisterWithPolicyUserSpecificID(request)
            });
        }
        [HttpPost("register-policy-hasclaim")]
        public async Task<IActionResult> RegisterWithPolicyHasClaimAsAdmin([FromBody] RegisterDto request){
            return Ok(new Response<AuthenticationResponseDto> {
                Message = "Successfully registered admin.",
                Data = await _authenticationService.RegisterWithPolicyHasClaimAsAdmin(request)
            });
        }
    }
}