using System.ComponentModel.DataAnnotations;

namespace Lanthanum.Web.Models
{
    public class ResetPasswordRequestModel
    {
        [Required(ErrorMessage = "Input your email")]
        public string Email { get; set; }
    }
}