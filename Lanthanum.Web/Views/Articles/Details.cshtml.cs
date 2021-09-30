using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lanthanum.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lanthanum.Web.Views.Articles
{
    public class DetailsModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
    