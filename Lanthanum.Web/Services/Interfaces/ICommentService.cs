using Lanthanum.Web.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lanthanum.Web.Services.Interfaces
{
    public interface ICommentService
    {
        public List<Comment> GetCurrentArticleComments(Article article);
        public List<Reaction> GetCommentReactions(List<Comment> comments);
        public List<User> GetCommentAuthors(List<Comment> comments);
        public List<Comment> SortComments(List<Comment> comments, string commentSortingMethod);
        public void AddComment(string commentContent, int articleId, string currentUserEmail, int parentCommentId = -1);
        public void DeleteComment(int commentId);
        public void ReactionManager(string currentUserEmail, int commentId, int reactionPoint);
        public void EditComment(int commentId, string newContent);
        public string GetCurrentUserImage(string currentUserEmail);

    }
    
}
