using System.ComponentModel.DataAnnotations;
using static HCM.Common.ValidationConstants.User;

namespace HCM.Web.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = UsernameRequiredMessage)]
        [StringLength(UsernameMaxLength, MinimumLength = UsernameMinLength)]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = PasswordRequiredMessage)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
