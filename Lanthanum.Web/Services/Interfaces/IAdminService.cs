using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lanthanum.Web.Domain;
using Lanthanum.Web.Models;

namespace Lanthanum.Web.Services.Interfaces
{
    public interface  IAdminService
    {
        public Task<IEnumerable<Article>> GetAllArticlesAsync();
        public Task DeleteArticleByIdAsync(int id);
        public Task ChangeArticleStatusByIdAsync(int id);

        public IEnumerable<ArticleViewModel> FilterArticles(ref IEnumerable<ArticleViewModel> articlesToViewModels,
            params string[] filterParams);
    }
}
