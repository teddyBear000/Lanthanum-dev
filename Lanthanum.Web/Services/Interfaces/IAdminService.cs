using System.Collections.Generic;
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
        public Task<Dictionary<int, string>> GetAllKindsOfSportNamesAsync();
        public Task ChangeArticleKindOfSportByIdAsync(int articleId, int kindOfSportId);
    }
}
