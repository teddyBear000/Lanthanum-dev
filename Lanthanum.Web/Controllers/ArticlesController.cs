using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Lanthanum.Web.Data.Repositories;
using Lanthanum.Web.Domain;
using Lanthanum.Web.Models;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using Lanthanum.Web.Services;

namespace Lanthanum.Web.Controllers
{
    public class ArticlesController : Controller
    {

        private readonly ILogger<ArticlesController> _logger;
        private readonly DbRepository<User> _userRepository;
        private readonly DbRepository<Article> _articleRepository;
        private readonly CommentService _commentService;
        public ArticlesController(ILogger<ArticlesController> logger, DbRepository<Comment> commentRepository, DbRepository<User> userRepository, DbRepository<Article> articleRepository,DbRepository<Reaction>reactionRepository,CommentService commentService)
        {
            _logger = logger;
            _userRepository = userRepository;
            _articleRepository = articleRepository;
            _commentService = commentService;
        }
        public IActionResult Details(int id, string commentSortingMethod = " ")
        {
            var article = _articleRepository.GetByIdAsync(id).Result;
            var comments = _commentService.GetCurrentArticleComments(article);
            var reactions = _commentService.GetCommentReactions(comments);
            var users = _commentService.GetCommentAuthors(comments);
            var sortedComments = _commentService.SortComments(comments, commentSortingMethod);
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
                ReactionToComments = reactions,
                MainArticle = article,
                Comments = sortedComments,
                Users = users,
                CurrentUserImage = currentUserImage,
                MoreArticlesSection = new List<Article>() { articleList[0], articleList[0], articleList[0], articleList[0], articleList[0], articleList[0] }
            };
           
            return View(model);
        }

        [Authorize]
        public IActionResult AddComment(string commentContent, int articleId, int parentCommentId = -1)
        {
            var author = _userRepository.SingleOrDefaultAsync(x => x.Email == User.Identity.Name).Result;
            _commentService.AddComment(commentContent, articleId, author, parentCommentId);
            return RedirectToAction("Details", new { id = articleId });
        }

        [Authorize]
        public IActionResult DeleteComment(int articleId, int commentId) 
        {
            _commentService.DeleteComment(commentId);
            return RedirectToAction("Details", new { id = articleId });
        }

        [Authorize]
        public IActionResult ManageReaction(int articleId, int commentId, int reactionPoint)
        {
            var author = _userRepository.SingleOrDefaultAsync(x => x.Email == User.Identity.Name).Result;
            _commentService.ReactionManager(author, commentId, reactionPoint);
            return RedirectToAction("Details", new { id = articleId });
        }
        [Authorize]
        public IActionResult EditComment(int articleId, int commentId, string commentNewContent) 
        {
            
            _commentService.EditComment(commentId, commentNewContent);
            return RedirectToAction("Details", new { id = articleId });
        }
    }
}