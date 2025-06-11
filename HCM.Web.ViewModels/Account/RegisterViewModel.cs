using System.ComponentModel.DataAnnotations;
using static HCM.Common.ValidationConstants.User;

namespace HCM.Web.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = UsernameRequiredMessage)]
        [StringLength(UsernameMaxLength, MinimumLength = UsernameMinLength)]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = EmailRequiredMessage)]
        [EmailAddress(ErrorMessage = EmailInvalidMessage)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = PasswordRequiredMessage)]
        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength, ErrorMessage = PasswordLengthMessage)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = ConfirmPasswordRequiredMessage)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = ConfirmPasswordMatchMessage)]
        public string ConfirmPassword { get; set; } = null!;
    }
}
