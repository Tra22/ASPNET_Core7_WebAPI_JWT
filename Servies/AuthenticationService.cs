using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using ASPNET_Core7_WebAPI_JWT.Constant;
using ASPNET_Core7_WebAPI_JWT.Dtos.Authentication;
using ASPNET_Core7_WebAPI_JWT.Entities;
using ASPNET_Core7_WebAPI_JWT.Model.Identity;
using ASPNET_Core7_WebAPI_JWT.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace ASPNET_Core7_WebAPI_JWT.Services {
    public class AuthenticationService : IAuthenticationService
    {
        private readonly DataContext _context;
        private readonly UserManager<AuthenticationUser> _userManager;
        private readonly RoleManager<AuthenticationRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticationService(
            DataContext context,
            UserManager<AuthenticationUser> userManager,
            RoleManager<AuthenticationRole> roleManager,
            IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        public async Task<AuthenticationResponseDto> Authenticate(AuthenticationRequestDto request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user is not null && await _userManager.CheckPasswordAsync(user, request.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new(ClaimTypes.Name, request.Username),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                authClaims.AddRange(await _userManager.GetClaimsAsync(user));

               var token = GetToken(authClaims);

                return new AuthenticationResponseDto{
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo
                };
            }
            throw new AuthenticationException("Fail to authenticate!");
        }

        public async Task<AuthenticationResponseDto> RegisterAsAdmin(RegisterDto request)
        {
            AuthenticationUser user = await RegisterUser(request, UserRoles.Admin);
            return await AuthenticateAfterRegister(request);
        }

        public async Task<AuthenticationResponseDto> RegisterAsUser(RegisterDto request)
        {
            AuthenticationUser user = await RegisterUser(request, UserRoles.User);
            return await AuthenticateAfterRegister(request);
        }

        public async Task<AuthenticationResponseDto> RegisterWithPolicyUserOnly(RegisterDto request)
        {
            AuthenticationUser user = await RegisterUser(request, UserRoles.User, new List<Claim>{
               new Claim("UserNumberID", "1", ClaimValueTypes.Integer),
            });
            return await AuthenticateAfterRegister(request);
        }
        public async Task<AuthenticationResponseDto> RegisterWithPolicyUserSpecificID(RegisterDto request)
        {
            AuthenticationUser user = await RegisterUser(request, UserRoles.User, new List<Claim>{
               new Claim("UserNumberID", "111", ClaimValueTypes.Integer),
            });
            return await AuthenticateAfterRegister(request);
        }
        public async Task<AuthenticationResponseDto> RegisterWithPolicyHasClaimAsAdmin(RegisterDto request)
        {
            AuthenticationUser user = await RegisterUser(request, UserRoles.User, new List<Claim>{
               new Claim("Admin", "true", ClaimValueTypes.Boolean, "http://localhost:5000"),
            });
            return await AuthenticateAfterRegister(request);
        }
        private async Task<AuthenticationUser> RegisterUser(RegisterDto request, string roleName, List<Claim>? claims = null){
            using var transaction = _context.Database.BeginTransaction();
            try{
                var userExists = await _userManager.FindByNameAsync(request.Username);
                if (userExists is not null)
                    throw new Exception("User already exists!");

                AuthenticationUser user = new()
                {
                    Email = request.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = request.Username,
                    PhoneNumber = request.PhoneNumber
                };
                var result = await _userManager.CreateAsync(user, request.Password);
                if (!result.Succeeded)
                    throw new Exception("User creation failed! Please check user details and try again.");

                if (!await _roleManager.RoleExistsAsync(roleName))
                    await _roleManager.CreateAsync(new AuthenticationRole(roleName));

                if (await _roleManager.RoleExistsAsync(roleName)) {
                    await _userManager.AddToRoleAsync(user, roleName);
                }
                if(user.PhoneNumber is not null){
                    await _userManager.AddClaimAsync(user, new Claim("PhoneNumber", user.PhoneNumber));
                }
                if(claims is not null)
                    foreach(var claim in claims)
                        await _userManager.AddClaimAsync(user, claim);
                await transaction.CommitAsync();
                return user;
            }catch(Exception){
                await transaction.RollbackAsync();
                throw;
            }
        }
        private async Task<AuthenticationResponseDto> AuthenticateAfterRegister(RegisterDto request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user is not null && await _userManager.CheckPasswordAsync(user, request.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new(ClaimTypes.Name, request.Username),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                authClaims.AddRange(await _userManager.GetClaimsAsync(user));

               var token = GetToken(authClaims);

                return new AuthenticationResponseDto{
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo
                };
            }
            throw new AuthenticationException("Fail to authenticate!");
        }
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]??"default_secret"));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}