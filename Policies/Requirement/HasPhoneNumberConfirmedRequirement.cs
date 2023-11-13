using Microsoft.AspNetCore.Authorization;

namespace ASPNET_Core7_WebAPI_JWT.Policies.Requirement {
    public class HasPhoneNumberConfirmedRequirement : IAuthorizationRequirement {
        public HasPhoneNumberConfirmedRequirement(bool IsPhoneNumberConfirmed) =>
            this.IsPhoneNumberConfirmed = IsPhoneNumberConfirmed;

        public bool IsPhoneNumberConfirmed { get; }
    }
}