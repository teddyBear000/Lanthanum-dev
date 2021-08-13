using System.Collections.Generic;

namespace Lanthanum_web.Domain
{
    public class KindOfSport
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Article> Articles { get; set; }
        public List<Team> Teams { get; set; }
        public List<Subscription> Subscriptions { get; set; }
    }
}