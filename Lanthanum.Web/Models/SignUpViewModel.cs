using System.ComponentModel.DataAnnotations;

namespace Lanthanum.Web.Models
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "Input your first name")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Input your last name")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "Input your email")]
        public string Email { get; set; }
        
        [MinLength(8, ErrorMessage = "Password must contain at least 8 characters")]
        [RegularExpression("^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$", ErrorMessage = "Password must contain letters and digits")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}