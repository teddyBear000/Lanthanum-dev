using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Lanthanum.Web.Data.Domain;
using Lanthanum.Web.Data.Repositories;
using Lanthanum.Web.Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Lanthanum.Web.Services
{
    public class AuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DbRepository<User> _repository;

        public AuthService(IHttpContextAccessor httpContextAccessor, DbRepository<User> repository)
        {
            _httpContextAccessor = httpContextAccessor;
            _repository = repository;

        }
        
        public async Task<bool> Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new (ClaimsIdentity.DefaultNameClaimType, user.Email),
                new (ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            };

            var id = new ClaimsIdentity(
                claims, 
                "ApplicationCookie", 
                ClaimsIdentity.DefaultNameClaimType, 
                ClaimsIdentity.DefaultRoleClaimType
            );

            await _httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(id)
            );

            return true;
        }

        public async Task AddUserAsync(User user)
        {
            await _repository.AddAsync(user);
        }
        public User GetUserByEmail(string email)
        {
            return _repository
                .GetEntity()
                .Include(u => u.ExternalProvider)
                .SingleOrDefaultAsync(u => u.Email == email).Result;
        }

        public bool IsExternalProvider(User user)
        {
            return user.ExternalProvider.LoginProvider != null;
        }

        public async Task<User> ExternalUserIntializer(AuthenticateResult result)
        { 
            return await Task.Run(() =>
            new User()
            {
                FirstName = result.Principal.FindFirstValue(ClaimTypes.GivenName),
                LastName = result.Principal.FindFirstValue(ClaimTypes.Surname),
                Email = result.Principal.FindFirstValue(ClaimTypes.Email),
                IsBanned = false,
                CurrentState = CurrentStates.Offline,
                Role = RoleStates.User,
                Subscription = new Subscription(),
                Subscribers = new List<Subscription>(),
                PublishedArticles = new List<Article>(),
                AvatarImagePath = "default.jpg",
                ExternalProvider = new ExternalProviderUserInfo()
                {
                    LoginProvider = result.Principal.Identity.AuthenticationType,
                    NameId = result.Principal.FindFirstValue(ClaimTypes.NameIdentifier),
                }
            });
        }

        public bool Logout()
        {
            _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            
            return true;
        }
        
        public bool IsSignedIn(ClaimsPrincipal principal)
        {
            return principal?.Identities != null &&
                   principal.Identity.IsAuthenticated;
        }
    }
}