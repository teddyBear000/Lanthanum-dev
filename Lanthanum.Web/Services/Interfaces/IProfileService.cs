using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lanthanum.Web.Domain;

namespace Lanthanum.Web.Services.Interfaces
{
    public interface IProfileService
    {
        public User GetUserByEmail(string email);
        public void UpdateUserProfile(string sessionEmail, string firstName, string lastName, string email);

    }
}
