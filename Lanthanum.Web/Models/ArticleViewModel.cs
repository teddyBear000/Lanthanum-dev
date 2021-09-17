using Lanthanum.Web.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lanthanum.Web.Models
{
    public class ArticleViewModel
    {
        public Article MainArticle { get; set; }
        public List<Comment> Comments{get; set;}
        public List<User> Users { get; set; }
        public string CurrentUserImage { get; set; }
        public List<Article> MoreArticlesSection { get; set; }
        public List <Reaction> ReactionToComments { get; set; }
    }
}
