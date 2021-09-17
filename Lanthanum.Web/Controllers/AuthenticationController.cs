using System;
using System.Collections.Generic;
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
        private readonly IEmailSenderService _emailSenderService;

        public AuthenticationController(
            ILogger<HomeController> logger, 
            DbRepository<User> repository, 
            AuthService service,
            IEmailSenderService emailSenderService
        )
        {
            _logger = logger;
            _repository = repository;
            _service = service;
            _emailSenderService = emailSenderService;
        }
        
        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LogIn(LogInViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = _repository
                .SingleOrDefaultAsync(u => u.Email == model.Email)
                .Result;

            if (user != null && BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
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

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp(SignUpViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            
            // Looking for user with this email in database
            var existingUser = _repository
                .SingleOrDefaultAsync(u => u.Email == model.Email)
                .Result;

            // If user with this email is existing
            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "This email is already used.");

                return View(model);
            }

            var passwordHashed = BCrypt.Net.BCrypt.HashPassword(model.Password);

            var newUser = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                IsBanned = false,
                CurrentState = CurrentStates.Offline,
                PasswordHash = passwordHashed,
                Role = RoleStates.User,
                Subscription = new Subscription(),
                Subscribers = new List<Subscription>(),
                PublishedArticles = new List<Article>()
            };

            try
            {
                _repository.AddAsync(newUser).Wait();
            }
            catch(Exception ex)
            {
                _logger.LogError($"User {newUser.Email} not registered. {ex.Message}");
                
                return RedirectToAction("Error", "Home");
            }
            
            _logger.LogInformation($"User {newUser.Email} registered successfully, role {newUser.Role}.");
            
            _emailSenderService.SendWelcomeEmailAsync(newUser);

            return RedirectToAction("LogIn", "Authentication");
        }
    }
}
