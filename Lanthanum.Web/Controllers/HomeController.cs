using Lanthanum.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Lanthanum.Web.Data.Domain;
using Lanthanum.Web.Data.Repositories;

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

        private void sendMail()
        {
            var mailSender = new MailSender();
            mailSender.SendWelcome("ignars3@gmail.com");
        }
        public IActionResult Index()
        {
            sendMail();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}