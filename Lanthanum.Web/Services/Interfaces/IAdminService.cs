﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Lanthanum.Web.Domain;
using Lanthanum.Web.Models;
using Microsoft.AspNetCore.Http;

namespace Lanthanum.Web.Services.Interfaces
{
    public interface IAdminService
    {
        public Task<IEnumerable<Article>> GetAllArticlesAsync();
        public Task DeleteArticleByIdAsync(int id);
        public Task ChangeArticleStatusByIdAsync(int id);
        public Task<AdminArticleViewModel> FilterArticlesAsync(AdminArticleViewModel articlesToViewModels,
            ISession session);
        public Task<Article> GetArticleByIdAsync(int id);
        public Task<Dictionary<int, string>> GetAllKindsOfSportNamesAsync();
        public Task ChangeArticleKindOfSportByIdAsync(int articleId, int kindOfSportId);
        public Task<AdminArticleViewModel> ViewModelInitializerAsync(ISession session);
        public Task FilterInitializerAsync(ISession session, params string[] filterParams);
    }
}