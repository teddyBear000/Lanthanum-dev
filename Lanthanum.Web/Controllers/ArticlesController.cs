using Lanthanum.Web.Data.Repositories;
using Lanthanum.Web.Domain;
using Lanthanum.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lanthanum.Web.Controllers
{
    public class ArticlesController : Controller
    {

        private readonly ILogger<ArticlesController> _logger;
        private readonly DbRepository<Comment> _commentRepository;
        private readonly DbRepository<User> _userRepository;
        private readonly DbRepository<Article> _articleRepository;
        public ArticlesController(ILogger<ArticlesController> logger, DbRepository<Comment> commentRepository, DbRepository<User> userRepository, DbRepository<Article> articleRepository)
        {
            _logger = logger;
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _articleRepository = articleRepository;
        }


        public IActionResult Details(int id, string commentSortingMethod=" ")
        {
            var article =  _articleRepository.GetByIdAsync(id).Result;
            var comments = _commentRepository
                .Find(x => x.Article == _articleRepository.GetByIdAsync(id).Result)
                .ToList();
            switch (commentSortingMethod) 
            {
                case "SortingMethodNewest":
                    comments.Sort((x, y) => y.DateTimeOfCreation.CompareTo(x.DateTimeOfCreation));
                    break;
                case "SortingMethodOldest":
                    comments.Sort((x, y) => x.DateTimeOfCreation.CompareTo(y.DateTimeOfCreation));
                    break;
                /*case "SortingMethodPopularity":
                    break;*/
                default:
                    comments.Sort((x, y) => y.DateTimeOfCreation.CompareTo(x.DateTimeOfCreation));
                    break;
            }
            var users = new List<User>();
            foreach(var userElement in _userRepository.GetAllAsync().Result)
            {
                foreach(var commentElement in comments)
                {
                    if (userElement == commentElement.Author)
                    {
                        users.Add(userElement);
                    }
                }
            }

            List<Article> articleList = _articleRepository.GetAllAsync().Result.ToList();
            var currentUserImage = "/content/userAvatars/";

            try 
            { 
                var temp = _userRepository.SingleOrDefaultAsync(x => x.Email == User.Identity.Name).Result; 

                if (temp != null)
                {
                    currentUserImage += temp.AvatarImagePath;
                }
                else 
                {
                    throw new Exception("Not Authorized");
                }
            }
            catch (Exception e)
            { 
                currentUserImage = "";
            }

            var model = new ArticleViewModel
            {
                MainArticle = article,
                Comments = comments,
                Users = users,
                CurrentUserImage = currentUserImage,
                MoreArticlesSection = new List<Article>() { articleList[0], articleList[0], articleList[0], articleList[0], articleList[0], articleList[0] }
            };
           
            return View(model);
        }
        

        [Authorize]
        public IActionResult AddComment(string commentContent, int articleId,int parentCommentId=-1)
        {
            var comment = new Comment
            {
                Content = commentContent,
                Author = _userRepository.SingleOrDefaultAsync(x => x.Email == User.Identity.Name).Result,
                Article = _articleRepository.GetByIdAsync(articleId).Result,
                ParentComment = _commentRepository.GetByIdAsync(parentCommentId).Result
            };
            _commentRepository.AddAsync(comment).Wait();
            return RedirectToAction("Details", new { id = articleId });
        }
    }
}