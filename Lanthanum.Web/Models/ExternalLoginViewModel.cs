using System.ComponentModel.DataAnnotations;

namespace Lanthanum.Web.Models
{
    public class ExternalLoginViewModel
    {
        [EmailAddress]
        public string Email { get; set; }
        public string ReturnUrl { get; set; }
        public string ProviderName { get; set; }      
    }

    public enum ExternalProvider
    {
        Google,Facebook
    }
}
