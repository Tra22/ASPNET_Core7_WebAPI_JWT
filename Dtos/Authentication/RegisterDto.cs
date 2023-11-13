using System.ComponentModel.DataAnnotations;

namespace ASPNET_Core7_WebAPI_JWT.Dtos.Authentication {
    public class RegisterDto {
        [Required(ErrorMessage = "User Name is required")]
        public required string Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public required string Password { get; set; }
        public string? PhoneNumber {get;set;}
        public bool IsPhoneVerify {get;set;} = false;
    }
}