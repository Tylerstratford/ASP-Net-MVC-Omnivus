using System.ComponentModel.DataAnnotations;

namespace ASP_Net_MVC.Models
{
    public class SignInForm
    {
        public SignInForm()
        {
            Email = "";
            Password = "";
            ReturnUrl = "/";
        }

        [Display(Name = "Email address")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid email")]
        [Required(ErrorMessage = "Please enter your email name")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter a password")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Password must meet the minimum requirements")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }

    }
}
