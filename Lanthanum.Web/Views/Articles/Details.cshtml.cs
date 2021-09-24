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
    