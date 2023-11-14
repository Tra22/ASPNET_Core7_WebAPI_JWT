using Asp.Versioning;
using ASPNET_Core7_WebAPI_JWT.Payload.Global;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET_Core7_WebAPI_JWT.Controllers{
    [AllowAnonymous]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AnonymousController : ControllerBase {
        [HttpGet]
        public IActionResult AnonymouseIndex() {
            return Ok(new Response<string>{
                Message = "Successfully",
                Data = "Anonymous access"
            });
        }
    }
}