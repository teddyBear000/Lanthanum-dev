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
        public IEnumerable<Team> Teams { get; set; }
        public IEnumerable<Conference> Conferences { get; set; }
        public IEnumerable<KindOfSport> KindsOfSport { get; set; }
        public IEnumerable<String> Locations => new List<String> { "Brazil", "England", "Italy", "France", "China", "Europe", "Asia" };
        public string SelectedSport { get; set; }
    }
}