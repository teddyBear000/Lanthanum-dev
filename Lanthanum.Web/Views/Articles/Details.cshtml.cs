using Lanthanum.Web.Data.Domain;
using Lanthanum.Web.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lanthanum.Web.Views.Articles
{
    public class DetailsModel : PageModel
    {
        private readonly DbRepository<Article> _dbRepository;
        public Article Article { get; private set; }

        public DetailsModel(DbRepository<Article> dbRepository)
        {
            _dbRepository = dbRepository;
        }

        public IActionResult OnGet(int id)
        {
            Article = _dbRepository.GetByIdAsync(id).Result;

            if (Article == null)
            {
                return RedirectToPage("/Home");
            }

            return Page();
        }
    }
}
