using ASPNET_Core7_WebAPI_JWT.Dtos.Authentication;

namespace ASPNET_Core7_WebAPI_JWT.Services.Interface {
    public interface IAuthenticationService {
        Task<AuthenticationResponseDto> Authenticate(AuthenticationRequestDto request);
        Task<AuthenticationResponseDto> RegisterAsAdmin(RegisterDto request);
        Task<AuthenticationResponseDto> RegisterAsUser(RegisterDto request);
        Task<AuthenticationResponseDto> RegisterWithPolicyUserOnly(RegisterDto request);
        Task<AuthenticationResponseDto> RegisterWithPolicyUserSpecificID(RegisterDto request);
        Task<AuthenticationResponseDto> RegisterWithPolicyHasClaimAsAdmin(RegisterDto request);
    }
}