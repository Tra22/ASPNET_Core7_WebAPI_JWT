using Asp.Versioning;
using ASPNET_Core7_WebAPI_JWT.Payload.Global;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET_Core7_WebAPI_JWT.Controllers {
    [Authorize(Policy = "HasPhoneNumberAndConfirmed")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PolicyCustomController : ControllerBase {
        [HttpGet]
        public IActionResult HasPhoneNumberAndConfirmedIndex() {
            return Ok(new Response<string>{
                Message = "Successfully",
                Data = "Allow only when user has phone number and confirmed phone number."
            });
        }
    }
}