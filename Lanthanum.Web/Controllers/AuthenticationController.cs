using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lanthanum.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult LogIn()
        {
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }
    }
}
