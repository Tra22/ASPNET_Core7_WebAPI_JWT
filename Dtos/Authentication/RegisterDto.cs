using System.ComponentModel.DataAnnotations;

namespace ASPNET_Core7_WebAPI_JWT.Dtos.Authentication {
    public class RegisterDto {
        [Required(ErrorMessage = "User Name is required")]
        public required string Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(18, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "Please Use a Combination of Lowercase, Uppercase, Digits, Special Symbols Characters")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public required string Password { get; set; }
        public string? PhoneNumber {get;set;}
        public bool IsPhoneVerify {get;set;} = false;
    }
}