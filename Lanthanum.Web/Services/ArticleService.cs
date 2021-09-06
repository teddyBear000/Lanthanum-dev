using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lanthanum.Web.Data.Repositories;
using Lanthanum.Web.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lanthanum.Web.Services
{
    public class ArticleService:IArticleService
    {
        private readonly DbRepository<Article> _repository;

        public ArticleService(DbRepository<Article> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Article>> GetAllArticles()
        {
            var articles = await _repository.GetAllAsync();
            return articles;
        }
    }
}
