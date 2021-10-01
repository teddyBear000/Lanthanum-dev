using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lanthanum.Web.Data.Repositories;
using Lanthanum.Web.Domain;
using Lanthanum.Web.Services.Interfaces;

namespace Lanthanum.Web.Services
{
    public class ProfileService : IProfileService
    {
        private readonly DbRepository<User> _userRepository;

        public ProfileService(DbRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetUserByEmail(string email)
        {
            User user = _userRepository.SingleOrDefaultAsync(x => x.Email == email).Result;
            return user;
        }

        public void UpdateUserProfile(string sessionEmail, string firstName, string lastName, string email)
        {
            User user = _userRepository.SingleOrDefaultAsync(x => x.Email == sessionEmail).Result;
            if (firstName != null) { user.FirstName = firstName; }
            if (lastName != null) { user.LastName = lastName; }
            if (email != null) { user.Email = email; }
            _userRepository.UpdateAsync(user).Wait();
        }
    }
}
