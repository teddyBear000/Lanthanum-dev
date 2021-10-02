using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lanthanum.Web.Models
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "Input your new password")]
        [RegularExpression("^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$", ErrorMessage = "Password must contain letters and digits")]
        public string NewPassword { get; set; }
        
        [NotMapped]
        [Compare("NewPassword")]
        public string Password { get; set; }
    }
}