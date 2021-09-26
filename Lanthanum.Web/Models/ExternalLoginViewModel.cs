using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lanthanum.Web.Models
{
    public class ExternalLoginViewModel
    {
        [EmailAddress]
        public string Email { get; set; }
        public string ReturnUrl { get; set; }
        public ExternalProvider ProviderName { get; set; }      
    }

    public enum ExternalProvider
    {
        Google
    }
}
