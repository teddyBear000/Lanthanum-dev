using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lanthanum.Web.Domain;

namespace Lanthanum.Web.Data.Domain
{
    public class ExternalProviderUserInfo
    {
        public int Id { get; set; }
        public string LoginProvider { get; set; }
        public string NameId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
