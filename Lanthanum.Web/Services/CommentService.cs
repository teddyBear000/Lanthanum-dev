using Lanthanum.Web.Data.Repositories;
using Lanthanum.Web.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lanthanum.Web.Services
{
    public class CommentService
    {

        private readonly DbRepository<Comment> _commentRepository;
        private readonly DbRepository<User> _userRepository;
        private readonly DbRepository<Article> _articleRepository;
        private readonly DbRepository<Reaction> _reactionRepository;

        public CommentService(DbRepository<Comment> commentRepository, DbRepository<User> userRepository, DbRepository<Article> articleRepository, DbRepository<Reaction> reactionRepository)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _articleRepository = articleRepository;
            _reactionRepository = reactionRepository;
        }

        public List<Comment> GetCurrentArticleComments(Article article) 
        { 
            return _commentRepository
                .Find(x => x.Article == article)
                .OrderByDescending(x => x.DateTimeOfCreation)
                .ToList();
        }
        public List<Reaction> GetCommentReactions(List<Comment> comments) 
        {
            var reactions = new List<Reaction>();
            foreach (var comment in comments)
            {
                foreach (var reaction in _reactionRepository.GetAllAsync().Result)
                {
                    if (reaction.Comment == comment) reactions.Add(reaction);
                }
            }
            return reactions;
        }
        public List<User> GetCommentAuthors(List<Comment> comments) 
        {
            var users = new List<User>();
            foreach (var user in _userRepository.GetAllAsync().Result)
            {
                foreach (var comment in comments)
                {
                    if (user == comment.Author)
                    {
                        users.Add(user);
                    }
                }
            }
            return users;
        }
        
        public List<Comment> SortComments(List<Comment> comments, string commentSortingMethod) 
        {
            switch (commentSortingMethod)
            {
                case "SortingMethodNewest":
                    comments
                        .Sort((x, y) => y.DateTimeOfCreation
                        .CompareTo(x.DateTimeOfCreation));
                    break;
                case "SortingMethodOldest":
                    comments
                        .Sort((x, y) => x.DateTimeOfCreation
                        .CompareTo(y.DateTimeOfCreation));
                    break;
                case "SortingMethodPopularity":
                    comments
                        .Sort((x, y) => y.Rate.CompareTo(x.Rate));
                    break;
                default:
                    comments
                        .Sort((x, y) => y.DateTimeOfCreation.CompareTo(x.DateTimeOfCreation));
                    break;
            }
            return comments;
        }

        public void AddComment(string commentContent, int articleId, User author, int parentCommentId = -1 ) 
        {
            var comment = new Comment
            {
                Content = commentContent,
                Author = author,
                Article = _articleRepository.GetByIdAsync(articleId).Result,
                ParentComment = _commentRepository.GetByIdAsync(parentCommentId).Result
            };
            _commentRepository.AddAsync(comment).Wait();
        }

        public void DeleteComment(int commentId) 
        {
            var commentToDelete = _commentRepository.GetByIdAsync(commentId).Result;
            var comments = _commentRepository.GetAllAsync().Result;
            foreach (var comment in comments)
            {
                if (comment.ParentComment == commentToDelete) _commentRepository.RemoveAsync(comment).Wait();
            }
            _commentRepository.RemoveAsync(commentToDelete).Wait();
        }

        public void ReactionManager(User author, int commentId, int reactionPoint) 
        {
            var comment = _commentRepository.GetByIdAsync(commentId).Result;

            //Check whether reaction exists, if not -> creating one
            if (_reactionRepository.SingleOrDefaultAsync(x => x.Author == author && x.Comment == comment).Result == null)
            {
                var reaction = new Reaction { Author = author, Comment = comment };
                if (reactionPoint == 1) { reaction.TypeOfReaction = ReactionType.Like; }
                else { reaction.TypeOfReaction = ReactionType.Dislike; }
                _reactionRepository.AddAsync(reaction).Wait();
            }
            //If "Like" reaction exists, checking whether user clicks Like or Dislike, Like -> deleting existing reaction, Dislike -> changing reaction type to "Dislike"
            else if (_reactionRepository.SingleOrDefaultAsync(x => x.Author == author && x.Comment == comment).Result.TypeOfReaction == ReactionType.Like)
            {
                if (reactionPoint == 1)
                {
                    _reactionRepository
                          .RemoveAsync(_reactionRepository.
                          SingleOrDefaultAsync(x => x.Author == author && x.Comment == comment).Result).Wait();
                }
                if (reactionPoint == 0)
                {
                    var reaction = _reactionRepository.SingleOrDefaultAsync(x => x.Author == author && x.Comment == comment).Result;
                    reaction.TypeOfReaction = ReactionType.Dislike;
                    _reactionRepository.UpdateAsync(reaction).Wait();
                }
            }
            //If "Like" reaction exists, checking whether user clicks Like or Dislike, Like -> deleting existing reaction, Dislike -> changing reaction type to "Dislike" 
            else if (_reactionRepository.SingleOrDefaultAsync(x => x.Author == author && x.Comment == comment).Result.TypeOfReaction == ReactionType.Dislike)
            {
                if (reactionPoint == 1)
                {
                    var reaction = _reactionRepository.SingleOrDefaultAsync(x => x.Author == author && x.Comment == comment).Result;
                    reaction.TypeOfReaction = ReactionType.Like;
                    _reactionRepository.UpdateAsync(reaction).Wait();
                }
                if (reactionPoint == 0)
                {
                    _reactionRepository
                        .RemoveAsync(_reactionRepository.
                        SingleOrDefaultAsync(x => x.Author == author && x.Comment == comment).Result).Wait();
                }
            }
        }

        public void EditComment(int commentId, string newContent) 
        {
            var comment = _commentRepository.GetByIdAsync(commentId).Result;
            comment.Content = newContent;
            _commentRepository.UpdateAsync(comment).Wait();
        }
    }
}
