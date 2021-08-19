using System.Collections.Generic;
using Lanthanum.Data;

namespace Lanthanum.Web.Domain
{
    public class KindOfSport: IEntity
    {
        public int Id { get; init; }
        public string Name { get; set; }

        public List<Article> Articles { get; set; }
        public List<Team> Teams { get; set; }
        public List<Subscription> Subscribers { get; set; }
    }
}