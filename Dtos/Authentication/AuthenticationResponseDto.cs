using System.ComponentModel.DataAnnotations;

namespace ASPNET_Core7_WebAPI_JWT.Dtos.Authentication {
    public class AuthenticationResponseDto {
        public DateTime Expiration {get;set;}
        public required string AccessToken {get;set;}
    }
}