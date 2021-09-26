using System.ComponentModel.DataAnnotations;

namespace Lanthanum.Web.Models
{
    public class LogInViewModel
    {
        [Required(ErrorMessage = "Input your email")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Input your password")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public ExternalProvider ExternalProvider { get; set; }
    }

}
