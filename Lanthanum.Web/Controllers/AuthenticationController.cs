using System;
using Microsoft.AspNetCore.Mvc;
using Lanthanum.Web.Data.Repositories;
using Lanthanum.Web.Domain;
using Lanthanum.Web.Models;
using Lanthanum.Web.Services;
using Microsoft.Extensions.Logging;

namespace Lanthanum.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbRepository<User> _repository;
        private readonly AuthService _service;

        public AuthenticationController(
            ILogger<HomeController> logger, 
            DbRepository<User> repository, 
            AuthService service)
        {
            _logger = logger;
            _repository = repository;
            _service = service;
        }
        
        public IActionResult LogIn()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LogIn(LogInViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            
            var passwordHashed = model.Password; // TODO: Hash password
            var user = _repository
                .SingleOrDefaultAsync(
                    u => u.Email == model.Email
                         && u.PasswordHash == passwordHashed
                         )
                .Result;

            if (user != null)
            {
                _service.Authenticate(user).Wait();
                user.CurrentState = CurrentStates.Online;
                    
                return RedirectToAction("Index", "Home");
            }
            
            // If wrong email and password
            ModelState.AddModelError(string.Empty, "Incorrect user ID or password. Try again.");
            
            // Adding red border for input fields
            ModelState.AddModelError("Email", string.Empty); 
            ModelState.AddModelError("Password", string.Empty);

            return View(model);
        }

        public IActionResult SignUp()
        {
            return View();
        }
    }
}
