using Asp.Versioning;
using ASPNET_Core7_WebAPI_JWT.Payload;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET_Core7_WebAPI_JWT.Controllers {
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ClaimBaseController : ControllerBase {
        [Authorize(Policy = "UserOnly")]
        [HttpGet]
        public IActionResult UserOnlyIndex(){
            return Ok(new Response<string>{
                Message = "Successfully",
                Data = "Policy User Only can access"
            });
        }
        [Authorize(Policy = "UserSpecificID")]
        [HttpGet("specific")]
        public IActionResult UserSpecificIDIndex(){
            return Ok(new Response<string>{
                Message = "Successfully",
                Data = "Policy User Specific ID (111,222) can access"
            });
        }
    }
}