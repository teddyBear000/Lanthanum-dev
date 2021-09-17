using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lanthanum.Web.Data.Repositories;
using Lanthanum.Web.Domain;
using Lanthanum.Web.Models;
using Lanthanum.Web.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace Lanthanum.Web.Services
{
    public class AdminService:IAdminService
    {
        private readonly DbRepository<Article> _repository;

        public AdminService(DbRepository<Article> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Article>> GetAllArticlesAsync()
        {
            var articles =  await _repository.GetAllAsync()
                .Include(t=>t.Team)
                .ToListAsync();
            return articles;
        }
        public async Task DeleteArticleByIdAsync(int id)
        {
            var article = await _repository.GetByIdAsync(id);
            await _repository.RemoveAsync(article);
        }
        public async Task ChangeArticleStatusByIdAsync(int id)
        {
            var article = await _repository.GetByIdAsync(id);
            article.ArticleStatus = article.ArticleStatus == ArticleStatus.Published ? 
                (ArticleStatus.Unpublished) : (ArticleStatus.Published);
            await _repository.Context.SaveChangesAsync();

        }

        /*
         params string[] filterParams - contains params to filter list of articles.
         Order of params is next: conferenceFilter, teamNameFilter, articleStatusFilter
        */
        public IEnumerable<ArticleViewModel> FilterArticles(ref IEnumerable<ArticleViewModel> articlesToViewModels, params string[] filterParams)
        {
            if (!string.IsNullOrEmpty(filterParams[0]) && filterParams[0] != "All")
            {
                articlesToViewModels = articlesToViewModels.Where(a => a.TeamConference == filterParams[0]);
            }
            if(!string.IsNullOrEmpty(filterParams[1]) && filterParams[1] != "All")
            {
                articlesToViewModels = articlesToViewModels.Where(a => a.TeamName == filterParams[1]);
            }

            if (!string.IsNullOrEmpty(filterParams[2]) && filterParams[2] != "All")
            {
                articlesToViewModels = articlesToViewModels.Where(a => a.ArticleStatus.ToString() == filterParams[2]);
            }

            return articlesToViewModels;
        }
    }
}
