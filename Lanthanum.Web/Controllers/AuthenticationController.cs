using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lanthanum.Web.Data.Repositories;
using Lanthanum.Web.Domain;
using Lanthanum.Web.Filters;
using Lanthanum.Web.Models;
using Lanthanum.Web.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Lanthanum.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AuthService _service;
        private readonly IEmailSenderService _emailSenderService;

        public AuthenticationController(
            ILogger<HomeController> logger, 
            AuthService service,
            IEmailSenderService emailSenderService
        )
        {
            _logger = logger;
            _service = service;
            _emailSenderService = emailSenderService;
        }

        [HttpPost] //TODO CHANGE THIS
        public IActionResult LogOut()
        {
            _service.Logout();
            return Ok();
        }

        /// <summary>
        /// Challenges consent screen of external provider(e.g google, facebook)
        /// and then redirects of ExternalLoginResponse.
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="returnUrl"></param>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public IActionResult ExternalLogin(string provider, string returnUrl, string viewName)
        {
            var redirectUri = Url.Action("ExternalLoginResponse", new
            {
                ReturnUrl = returnUrl,
                ViewName = viewName
                
            });
            var properties = new AuthenticationProperties
            {
                RedirectUri = redirectUri
            };
            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> ExternalLoginResponseAsync(string viewName, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            AuthenticateResult result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var provider = result.Principal.Identity.AuthenticationType;
            var email = result.Principal.FindFirstValue(ClaimTypes.Email);
            var user = _service.GetUserByEmail(email);

            if (user != null)
            {
                if (!_service.IsExternalProvider(user))
                {
                    ModelState.AddModelError(String.Empty, $"Can't log in through {provider} because you haven't signed up through third party providers.");
                    return View(viewName);
                }

                if (user.ExternalProvider.LoginProvider != provider)
                {
                    ModelState.AddModelError(String.Empty, $"Can't log in through {provider} because you have already signed up through {user.ExternalProvider.LoginProvider}.");
                    return View(viewName);
                }

                _service.Authenticate(user).Wait();
                user.CurrentState = CurrentStates.Online;
                return LocalRedirect(returnUrl);
            }

            var newUser = _service.ExternalUserIntializer(result).Result;

            try
            {
                await _service.AddUserAsync(newUser);
                await _service.Authenticate(newUser);
            }
            catch (Exception ex)
            {
                _logger.LogError($"User {newUser.Email} not registered. {ex.Message}.");

                return RedirectToAction("Error", "Home");
            }

            _logger.LogInformation($"User {newUser.Email} registered successfully, role {newUser.Role}.");
            await _emailSenderService.SendWelcomeEmailAsync(newUser);

            return LocalRedirect(returnUrl);
        }

        /// <param name="returnUrl">Preserves url which we tried to access before being authenticated.
        /// For example: trying to access admin/articles-list when not authenticated, we go to authentication/login,
        /// but preserving admin/articles-list, therefore we can be directed to this url.</param>.
        [HttpGet]
        public IActionResult LogIn(string returnUrl)
        {
            LogInViewModel model = new LogInViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LogIn(LogInViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = _service.GetUserByEmail(model.Email);

            if (user != null && _service.IsExternalProvider(user))
            {
                ModelState.AddModelError(String.Empty, $"Can't login because you are signed up through {user.ExternalProvider.LoginProvider}.");
                return View(model);
            }

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
            var existingUser = _service.GetUserByEmail(model.Email);

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
                PublishedArticles = new List<Article>(),
                AvatarImagePath = "default.jpg"
            };

            try
            {
                _service.AddUserAsync(newUser).Wait();
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
