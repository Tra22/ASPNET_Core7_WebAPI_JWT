using Microsoft.AspNetCore.Identity;

namespace ASPNET_Core7_WebAPI_JWT.Model.Identity {
    public class AuthenticationRole : IdentityRole
    {
        public AuthenticationRole() : base()
        {
        }
        public AuthenticationRole(string roleName) : base(roleName)
        {
        }
    }
}