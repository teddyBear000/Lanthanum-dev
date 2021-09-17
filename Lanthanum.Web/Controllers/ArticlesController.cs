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
        private readonly DbRepository<Reaction> _reactionRepository;
        public ArticlesController(ILogger<ArticlesController> logger, DbRepository<Comment> commentRepository, DbRepository<User> userRepository, DbRepository<Article> articleRepository,DbRepository<Reaction>reactionRepository)
        {
            _logger = logger;
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _articleRepository = articleRepository;
            _reactionRepository = reactionRepository;
        }
        public IActionResult Details(int id, string commentSortingMethod = " ")
        {
            var article = _articleRepository.GetByIdAsync(id).Result;
            var comments = _commentRepository
                .Find(x => x.Article == _articleRepository.GetByIdAsync(id).Result)
                .ToList();

            var reactions = new List<Reaction>();
            var users = new List<User>();
            foreach (var comment in comments) 
            { 
                foreach(var reaction in _reactionRepository.GetAllAsync().Result) 
                {
                    if (reaction.Comment == comment) reactions.Add(reaction);
                }
            }
            foreach (var userElement in _userRepository.GetAllAsync().Result)
            {
                foreach (var commentElement in comments)
                {
                    if (userElement == commentElement.Author)
                    {
                        users.Add(userElement);
                    }
                }
            }

            List<Article> articleList = _articleRepository.GetAllAsync().Result.ToList();
            var currentUserImage = "/content/userAvatars/";

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
                Comments = comments,
                Users = users,
                CurrentUserImage = currentUserImage,
                MoreArticlesSection = new List<Article>() { articleList[0], articleList[0], articleList[0], articleList[0], articleList[0], articleList[0] }
            };

            return View(model);
        }


        [Authorize]
        public IActionResult AddComment(string commentContent, int articleId, int parentCommentId = -1)
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




        [Authorize]
        public IActionResult ManageReaction(int articleId, int commentId, int rate)
        {
            var author = _userRepository.SingleOrDefaultAsync(x => x.Email == User.Identity.Name).Result;
            var comment = _commentRepository.SingleOrDefaultAsync(x => x.Id == commentId).Result;
            //Check whether reaction exists, if not -> creating one
            if (_reactionRepository.SingleOrDefaultAsync(x => x.Author == author&&x.Comment==comment).Result == null) 
            {
                var reaction = new Reaction { Author = author, Comment = comment };
                if (rate == 1) { reaction.TypeOfReaction = ReactionType.Like; }
                else { reaction.TypeOfReaction = ReactionType.Dislike; }
                _reactionRepository.AddAsync(reaction).Wait();
            }
            //If "Like" reaction exists, checking whether user clicks Like or Dislike, Like -> deleting existing reaction, Dislike -> changing reaction type to "Dislike"
            else if (_reactionRepository.SingleOrDefaultAsync(x => x.Author == author && x.Comment == comment).Result.TypeOfReaction == ReactionType.Like)  
            {
                if (rate == 1) 
                { _reactionRepository
                        .RemoveAsync(_reactionRepository.
                        SingleOrDefaultAsync(x => x.Author == author && x.Comment == comment).Result).Wait(); 
                }
                if (rate == 0) 
                {
                    var reaction = _reactionRepository.SingleOrDefaultAsync(x => x.Author == author && x.Comment == comment).Result;
                    reaction.TypeOfReaction = ReactionType.Dislike;
                    _reactionRepository.UpdateAsync(reaction).Wait();
                }
            }
            //If "Like" reaction exists, checking whether user clicks Like or Dislike, Like -> deleting existing reaction, Dislike -> changing reaction type to "Dislike" 
            else if (_reactionRepository.SingleOrDefaultAsync(x => x.Author == author && x.Comment == comment).Result.TypeOfReaction == ReactionType.Dislike) 
            {
                if (rate == 1)
                {
                    var reaction = _reactionRepository.SingleOrDefaultAsync(x => x.Author == author && x.Comment == comment).Result;
                    reaction.TypeOfReaction = ReactionType.Like;
                    _reactionRepository.UpdateAsync(reaction).Wait();
                }
                if (rate == 0)
                {
                    _reactionRepository
                        .RemoveAsync(_reactionRepository.
                        SingleOrDefaultAsync(x => x.Author == author && x.Comment == comment).Result).Wait();
                }
            }
            return RedirectToAction("Details", new { id = articleId });
        }
    }
}