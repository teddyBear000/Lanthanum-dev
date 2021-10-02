#nullable enable
using System;
using System.Collections.Generic;
using Lanthanum.Data.Domain;
using Microsoft.AspNetCore.Mvc;
using Lanthanum.Data.Repositories;
using Lanthanum.Web.Models;
using Lanthanum.Web.Services;
using Microsoft.Extensions.Logging;

namespace Lanthanum.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ActionRequestService _actionRequestService;
        private readonly ILogger<HomeController> _logger;
        private readonly DbRepository<User> _repository;
        private readonly AuthService _service;
        private readonly IEmailSenderService _emailSenderService;
        private readonly UserService _userService;

        public AuthenticationController(
            ILogger<HomeController> logger, 
            DbRepository<User> repository, 
            AuthService service,
            IEmailSenderService emailSenderService, 
            ActionRequestService actionRequestService,
            UserService userService)
        {
            _actionRequestService = actionRequestService;
            _logger = logger;
            _repository = repository;
            _service = service;
            _emailSenderService = emailSenderService;
            _userService = userService;
        }
        
        [HttpGet]
        public IActionResult LogIn(string? message)
        {
            ViewBag.Message = message ?? string.Empty;
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

            var newUser = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                IsBanned = false,
                CurrentState = CurrentStates.Offline,
                Role = RoleStates.User,
                Subscription = new Subscription(),
                Subscribers = new List<Subscription>(),
                PublishedArticles = new List<Article>(),
                AvatarImagePath = "default.jpg"
            };

            try
            {
                _repository.AddAsync(newUser).Wait();
                _userService.ChangePasswordAsync(newUser, model.Password).Wait();
            }
            catch(Exception e)
            {
                _logger.LogError($"User {newUser.Email} not registered. {e.Message}");
                
                return RedirectToAction("Error", "Home");
            }
            
            _logger.LogInformation($"User {newUser.Email} registered successfully, role {newUser.Role}.");
            
            _emailSenderService.SendWelcomeEmailAsync(
                newUser.Email, 
                Url.Action("Index", "Home", null, protocol: HttpContext.Request.Scheme)
                );

            return RedirectToAction("LogIn", "Authentication");
        }

        [HttpGet]
        public IActionResult ResetPassword(string requestCode)
        {
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordModel model, string requestCode)
        {
            if (!ModelState.IsValid) return View(model);

            ActionRequest request;
            try
            {
                request = _actionRequestService.GetThenRemoveActionRequestAsync(requestCode).Result;
            }
            catch (Exception e)
            {
                _logger.LogError($"Request code {requestCode} not retrieve. {e.Message}");
                return RedirectToAction("Error", "Home");
            }

            _userService.ChangePasswordAsync(request.RequestOwner, model.NewPassword).Wait();
            
            return RedirectToAction("LogIn", "Authentication", new {message = "Your password has been updated."});
        }
        
        [HttpGet]
        public IActionResult ResetPasswordRequest()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult SuccessfulResetPasswordRequest(string email)
        {
            ViewBag.Email = email;
            return View();
        }
        
        [HttpPost]
        public IActionResult ResetPasswordRequest(ResetPasswordRequestModel model)
        {
            try
            {
                var user = _repository.SingleOrDefaultAsync(u => u.Email == model.Email).Result;
                var request  = _actionRequestService.CreateActionRequestCodeAsync(user).Result;

                var url = Url.Action("ResetPassword", "Authentication", 
                    new { requestCode = request.RequestCode },
                    protocol: HttpContext.Request.Scheme);
                _emailSenderService.SendResetPasswordRequestAsync(user.Email, url);
            }
            catch (Exception e)
            {
                _logger.LogError($"Reset password email to {model.Email} not sent. {e.Message}");
            }
            
            return RedirectToAction(
                "SuccessfulResetPasswordRequest", 
                "Authentication", 
                new { email = model.Email }
                );
        }

    }
}
