using ASPNET_Core7_WebAPI_JWT.Payload.Global;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ASPNET_Core7_WebAPI_JWT.Filter {
    public class ValidateModelAttribute: ActionFilterAttribute
    {   
        public override void OnActionExecuting(ActionExecutingContext context)  
        {  
            if (!context.ModelState.IsValid)  
            {  
                context.Result = new BadRequestObjectResult(new ValidationResultModel(context.ModelState))
                    {
                        ContentTypes = { "application/problem+json" }
                    };
            }  
        }  
    }  
}