using Asp.Versioning;
using ASPNET_Core7_WebAPI_JWT.Payload;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET_Core7_WebAPI_JWT.Controllers {
    [Authorize(Roles = "Administrator, User")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class RoleBaseController : ControllerBase {
        [HttpGet("user")]
        public IActionResult Index() {
            return Ok(new Response<string>{
                Message = "Successfully",
                Data = "Roles Access: Administrator || User"
            });
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet("admin")]
        public IActionResult AdministratorOnly(){
            return Ok(new Response<string>{
                Message = "Successfully",
                Data = "Roles Access: Administrator Only"
            });
        }
    }
}