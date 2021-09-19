using System.Collections.Generic;
using System.Linq;
using Lanthanum.Web.Domain;

namespace Lanthanum.Web.Models
{
    public class AdminArticleViewModel
    {
        public IEnumerable<HelperAdminArticleViewModel> SimpleModels { get; set; }
        public string FilterConference { get; set; }
        public string FilterTeam { get; set; }
        public string FilterStatus { get; set; }
        public string SearchString { get; set; }
        public IEnumerable<string> TeamNames { get; set; }
        public IEnumerable<string> Conferences { get; set; }
        public Dictionary<int,string> KindsOfSport { get; set; }

    }
}
