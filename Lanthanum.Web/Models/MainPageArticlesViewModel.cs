using Lanthanum.Web.Domain;
using System.Collections.Generic;


namespace Lanthanum.Web.Models
{
    public class MainPageArticlesViewModel
    {
        public List<Article> MainArticles { get; set; }
        public List<Article> AdditionalArticles { get; set; }
    }
}
