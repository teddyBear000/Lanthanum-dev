using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lanthanum.Web.Data.Repositories;
using Lanthanum.Web.Domain;
using Lanthanum.Web.Models;
using Lanthanum.Web.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
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

        /// <summary>
        /// Challenges consent screen of external provider(e.g google, facebook)
        /// and then redirects of ExternalLoginResponse.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IActionResult ExternalLogin(LogInViewModel model)
        {
            var redirectUri = Url.Action("ExternalLoginResponse", new
            {
                ReturnUrl = model.ReturnUrl
            });
            var properties = new AuthenticationProperties
            {
                RedirectUri = redirectUri
            };
            var provider = model.ExternalProvider.ToString();
            return new ChallengeResult(provider,properties);
        }

        public async Task<IActionResult> ExternalLoginResponse()
        {

            AuthenticateResult result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            //var claims = result.Principal.Identities
            //    .FirstOrDefault().Claims.Select(claim => new
            //    {
            //        claim.Issuer,
            //        claim.OriginalIssuer,
            //        claim.Type,
            //        claim.Value
            //    });

            var issuer = result.Principal.Identity.AuthenticationType;
            var userIdentifier = result.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
            var email = result.Principal.FindFirstValue(ClaimTypes.Email);
            //var firstName = result.Principal.FindFirstValue(ClaimTypes.g)
            var fname = result.Principal.FindFirstValue(ClaimTypes.GivenName);


            var user = _repository
                .SingleOrDefaultAsync(u => u.Email == email)
                .Result;

            return Json("halahala");
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
                PublishedArticles = new List<Article>(),
                AvatarImagePath = "default.jpg"
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
