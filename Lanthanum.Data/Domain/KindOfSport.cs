using System.Collections.Generic;

namespace Lanthanum.Web.Data.Domain
{
    public class KindOfSport : IEntity
    {
        public int Id { get; init; }
        public string Name { get; set; }

        public List<Article> Articles { get; set; }
        public List<Team> Teams { get; set; }
        public List<Subscription> Subscribers { get; set; }
    }
}