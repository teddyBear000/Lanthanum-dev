using Lanthanum.Web.Data.Domain;
using Lanthanum.Web.Data.Repositories;
using Lanthanum.Web.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lanthanum.Web.Views.Articles
{
    public class DetailsModel : PageModel
    {
        public IActionResult OnGet()
            {
                return RedirectToPage("/Home");
            }

            return Page();
        }
    }
}
    