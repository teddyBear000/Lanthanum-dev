using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lanthanum.Web.Domain;

namespace Lanthanum.Web.Models
{
    public class ArticleViewModel
    {
        public string Headline { get; set; }
        public string LogoPath { get; set; }
        public string MainText { get; set; }
        public string Alt { get; set; }
        public string Caption { get; set; }
        //A had doubts about whether it better to pass Team or concrete properties from Team like location, name
        public string TeamName { get; set; }
        public string TeamConference { get; set; }
        public ArticleStatus ArticleStatus { get; set; }
    }
}
