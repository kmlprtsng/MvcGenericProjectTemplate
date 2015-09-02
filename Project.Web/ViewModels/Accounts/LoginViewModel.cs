using System.ComponentModel.DataAnnotations;

namespace Project.Web.ViewModels.Accounts
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; } 
    }
}