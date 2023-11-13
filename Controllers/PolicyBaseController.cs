using Asp.Versioning;
using ASPNET_Core7_WebAPI_JWT.Payload;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET_Core7_WebAPI_JWT.Controllers {
    [Authorize(Policy = "HasClaimAsAdmin")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PolicyBaseController : ControllerBase {
        [HttpGet]
        public IActionResult HasClaimAsAdminIndex() {
            return Ok(new Response<string>{
                Message = "Successfully",
                Data = "Has Claim As Admin || SuperAdmin can access"
            });
        }
    }
}