using Lanthanum.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Lanthanum.Web.Data.Repositories;
using Lanthanum.Web.Domain;
using Microsoft.EntityFrameworkCore;

namespace Lanthanum.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbRepository<User> _userRepository;
        private readonly DbRepository<Article> _articleRepository;
        private readonly DbRepository<Comment> _commentRepository;
        public HomeController(ILogger<HomeController> logger, DbRepository<User> userRepository, DbRepository<Article> articleRepository, DbRepository<Comment> commentRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _articleRepository = articleRepository;
            _commentRepository = commentRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        // TODO: Move
        private void AddAdmin()
        {
            const string email = "mail@gmail.com"; // admin email
            if (_userRepository.SingleOrDefaultAsync(x => x.Email == email).Result == null)
            {
                _userRepository.AddAsync(new User
                {
                    Email = email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("12345678"),
                    Role = RoleStates.Admin
                }).Wait();
            }
        }
    }
}