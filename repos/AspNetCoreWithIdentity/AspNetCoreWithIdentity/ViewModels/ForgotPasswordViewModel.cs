using System.ComponentModel.DataAnnotations;

namespace AspNetCoreWithIdentity.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
