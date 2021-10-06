using Lanthanum.Web.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lanthanum.Web.Models
{
    public class AddingArticleViewModel
    {
        public List<Team> Teams { get; set; }
        public string SelectedSport { get; set; }
    }
}