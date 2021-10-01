using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lanthanum.Web.Domain;
using Lanthanum.Web.Models;
using Lanthanum.Web.Services;
using Lanthanum.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Lanthanum.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;
        private readonly AuthService _authService;

        public ProfileController(IProfileService profileService, AuthService authService)
        {
            _profileService = profileService;
            _authService = authService;
        }

        [Authorize]
        public IActionResult Personal()
        {
            User user = _profileService.GetUserByEmail(User.Identity.Name);//session email
            var model = new ProfileViewModel
            {
                User = user
            };
            return View(model);
        }

        [Authorize]
        public IActionResult UpdateProfile(string firstName=null, string lastName=null, string email=null)
        {
            string sessionEmail = User.Identity.Name;
            _authService.Logout();
            _profileService.UpdateUserProfile(sessionEmail,firstName,lastName,email);
            if(email==null)email = sessionEmail;
            User user = _profileService.GetUserByEmail(email);
            _authService.Authenticate(user).Wait();
            return RedirectToAction("Personal");
        }
    }
}
