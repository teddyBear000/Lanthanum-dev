using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lanthanum.Web.Data.Repositories;
using Lanthanum.Web.Domain;
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
        public async Task ChangeArticleStateByIdAsync(int id)
        {
            var article = await _repository.GetByIdAsync(id);
            article.ArticleStatus = article.ArticleStatus == ArticleStatus.Published ? 
                (ArticleStatus.Unpublished) : (ArticleStatus.Published);
            await _repository.Context.SaveChangesAsync();

        }
    }
}
