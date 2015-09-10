using System.ComponentModel.DataAnnotations;

namespace Project.Web.ViewModels.Accounts
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}