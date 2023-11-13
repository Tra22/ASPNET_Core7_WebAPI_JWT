using Asp.Versioning;
using ASPNET_Core7_WebAPI_JWT.Payload;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET_Core7_WebAPI_JWT.Controllers{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProtectController : ControllerBase {
        [HttpGet]
        public IActionResult ProtectIndex(){
            return Ok(new Response<string>{
                Message = "Successfully",
                Data = "Authorize for access"
            });
        }

    }
}