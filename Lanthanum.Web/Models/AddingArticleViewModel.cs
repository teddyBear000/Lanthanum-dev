using Lanthanum.Web.Data.Domain;
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
        public List<Conference> Conferences { get; set; }
        public List<KindOfSport> KindsOfSport { get; set; }
        public List<String> Locations => new List<String>() { "Brazil", "England", "Italy", "France", "China","Europe", "Asia"};
        public string SelectedSport { get; set; }
    }
}