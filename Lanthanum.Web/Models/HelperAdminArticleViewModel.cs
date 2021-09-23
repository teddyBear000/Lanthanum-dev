using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Lanthanum.Web.Domain;

namespace Lanthanum.Web.Models
{
    public class HelperAdminArticleViewModel
    {
        public int Id { get; set; }
        public string Headline { get; set; }
        public string LogoPath { get; set; }
        public string MainText { get; set; }
        public string Alt { get; set; }
        public string Caption { get; set; }
        public string TeamLocation { get; set; }
        public string TeamConference { get; set; }
        public string TeamName { get; set; }
        public string KindOfSportName { get; set; }
        public ArticleStatus ArticleStatus { get; set; }
    }
}
