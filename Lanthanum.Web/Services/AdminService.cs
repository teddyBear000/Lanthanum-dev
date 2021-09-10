using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lanthanum.Web.Data.Repositories;
using Lanthanum.Web.Domain;
using Lanthanum.Web.Services.Interfaces;

namespace Lanthanum.Web.Services
{
    public class AdminService:IAdminService
    {
        private readonly DbRepository<Article> _repository;

        public AdminService(DbRepository<Article> repository)
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
