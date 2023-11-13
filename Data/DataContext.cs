using ASPNET_Core7_WebAPI_JWT.Model.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ASPNET_Core7_WebAPI_JWT.Entities
{
    public class DataContext : IdentityDbContext<AuthenticationUser,  AuthenticationRole, string, IdentityUserClaim<string>, IdentityUserRole<string>,
    IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public DataContext(DbContextOptions<DataContext> options)
        : base(options){}
    }
}