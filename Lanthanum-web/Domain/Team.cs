using System;
using System.Collections.Generic;

namespace Lanthanum_web.Domain
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime DateTimeOfFoundation { get; set; }

        public KindOfSport KindOfSport { get; set; }
        public List<Article> Articles { get; set; }
        public List<Subscription> Subscriptions { get; set; }
    }
}