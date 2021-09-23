using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lanthanum.Web.Data.Repositories;
using Lanthanum.Web.Domain;
using Lanthanum.Web.Models;
using Lanthanum.Web.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Lanthanum.Web.Services
{
    public class AdminService:IAdminService
    {
        private readonly DbRepository<Article> _articleRepository;
        private readonly DbRepository<KindOfSport> _kindOfSportRepository;
        private readonly IMapper _mapper;

        public AdminService(DbRepository<Article> articleRepository, DbRepository<KindOfSport> kindOfSportRepository, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _kindOfSportRepository = kindOfSportRepository;
            _mapper = mapper;
        }

        /*
         Fetches all articles from article repository including relative entity - Teams.
        */
        public async Task<IEnumerable<Article>> GetAllArticlesAsync()
        {
            var articles =  await _articleRepository.GetEntity()
                .Include(t=>t.Team)
                .Include(k=>k.KindOfSport)
                .ToListAsync();
            return articles;
        }
        /*
         Deletes article from article repository and saves changes.
        */
        public async Task DeleteArticleByIdAsync(int id)
        {
            var article = await _articleRepository.GetByIdAsync(id);
            await _articleRepository.RemoveAsync(article);
        }
        /*
         Fetches article from article repository, changes ArticleStatus to opposite
        and saves changes.
        */
        public async Task ChangeArticleStatusByIdAsync(int id)
        {
            var article = await _articleRepository.GetByIdAsync(id);
            article.ArticleStatus = article.ArticleStatus == ArticleStatus.Published ? 
                (ArticleStatus.Unpublished) : (ArticleStatus.Published);
            await _articleRepository.UpdateAsync(article);

        }
     
        public AdminArticleViewModel FilterArticles(AdminArticleViewModel articlesToViewModels, ISession session)
        {
            if (!string.IsNullOrEmpty(session.GetString("FilterConference")) && session.GetString("FilterConference") != "All")
            {
                articlesToViewModels.SimpleModels = articlesToViewModels.SimpleModels.Where(a => a.TeamConference == session.GetString("FilterConference"));
            }
            if(!string.IsNullOrEmpty(session.GetString("FilterTeam")) && session.GetString("FilterTeam") != "All")
            {
                articlesToViewModels.SimpleModels = articlesToViewModels.SimpleModels.Where(a => a.TeamName == session.GetString("FilterTeam"));
            }

            if (!string.IsNullOrEmpty(session.GetString("FilterStatus")) && session.GetString("FilterStatus") != "All")
            {
                articlesToViewModels.SimpleModels = articlesToViewModels.SimpleModels.Where(a => a.ArticleStatus.ToString() == session.GetString("FilterStatus"));
            }

            if (!string.IsNullOrEmpty(session.GetString("SearchString")))
            {
                articlesToViewModels.SimpleModels = articlesToViewModels.SimpleModels.Where(
                    a => a.Headline.Contains(session.GetString("SearchString"))
                 || a.MainText.Contains(session.GetString("SearchString"))
                 || a.TeamName.Contains(session.GetString("SearchString"))
                 || a.TeamConference.Contains(session.GetString("SearchString"))
                 || a.TeamLocation.Contains(session.GetString("SearchString")));
            }
            return articlesToViewModels;
        }

        public async Task<Article> GetArticleByIdAsync(int id)
        {
             return await _articleRepository.GetByIdAsync(id);
        }
        public async Task<Dictionary<int,string>> GetAllKindsOfSportNamesAsync()
        {
            return await _kindOfSportRepository.GetEntity().ToDictionaryAsync(x=>x.Id, x=>x.Name);
        }
        public async Task ChangeArticleKindOfSportByIdAsync(int articleId, int kindOfSportId)
        {
            var article = await GetArticleByIdAsync(articleId);
            var kindOfSport = await _kindOfSportRepository.GetByIdAsync(kindOfSportId);
            article.KindOfSport = kindOfSport;
            await _articleRepository.UpdateAsync(article);
        }

        public async Task<AdminArticleViewModel> ViewModelInitializer(ISession session)
        {
            IEnumerable<Article> articles = await GetAllArticlesAsync();
            IEnumerable<HelperAdminArticleViewModel> helperModels = _mapper.Map<IEnumerable<HelperAdminArticleViewModel>>(articles);
            AdminArticleViewModel articlesToViewModels = new AdminArticleViewModel()
            {
                SimpleModels = helperModels,
                FilterConference = session.GetString("FilterConference"),
                FilterStatus = session.GetString("FilterStatus"),
                FilterTeam = session.GetString("FilterTeam"),
                SearchString = session.GetString("SearchString"),
                Conferences = helperModels.Select(a => a.TeamConference).Distinct(),
                TeamNames = helperModels.Select(a => a.TeamName).Distinct(),
                KindsOfSport = GetAllKindsOfSportNamesAsync().Result
            };
            return articlesToViewModels;
        }

        /*
         params string[] filterParams - contains params to filter list of articles.
         Order of params is next: conferenceFilter, teamNameFilter, articleStatusFilter, searchString.
        */
        public async Task FilterInitializer(ISession session, params string[] filterParams)
        {
           await Task.Run(() =>
                {
                    if (!string.IsNullOrEmpty(filterParams[0])) {  session.SetString("FilterConference", filterParams[0]); }
                    if (!string.IsNullOrEmpty(filterParams[1])) { session.SetString("FilterTeam", filterParams[1]); }
                    if (!string.IsNullOrEmpty(filterParams[2])) { session.SetString("FilterStatus", filterParams[2]); }
                    session.SetString("SearchString", !string.IsNullOrEmpty(filterParams[3]) ? filterParams[3] : "");
                }
            );
        }
    }
}
