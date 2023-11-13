using System.ComponentModel.DataAnnotations;

namespace ASPNET_Core7_WebAPI_JWT.Dtos.Authentication {
    public class AuthenticationRequestDto {
        [Required(ErrorMessage = "User Name is required")]
        public required string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public required string Password { get; set; }
    }
}