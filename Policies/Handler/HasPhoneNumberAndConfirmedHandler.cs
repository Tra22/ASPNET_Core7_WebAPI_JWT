using System.Security.Claims;
using ASPNET_Core7_WebAPI_JWT.Policies.Requirement;
using Microsoft.AspNetCore.Authorization;

namespace ASPNET_Core7_WebAPI_JWT.Policies.Handler {
    public class HasPhoneNumberAndConfirmedHandler : AuthorizationHandler<HasPhoneNumberConfirmedRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasPhoneNumberConfirmedRequirement requirement)
        {
            var phoneNumberAndConfirmed = context.User.FindFirst(c => 
                c.Type == "PhoneNumber");
            if (phoneNumberAndConfirmed is null) {
                return Task.CompletedTask;
            }else if(requirement.IsPhoneNumberConfirmed){
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}