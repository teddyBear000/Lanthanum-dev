using System;
using System.Collections.Generic;

namespace Lanthanum.Web.Data.Domain
{
    public class Team : IEntity
    {
        public int Id { get; init; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime DateTimeOfFoundation { get; set; }

        public KindOfSport KindOfSport { get; set; }
        public List<Article> Articles { get; set; }
        public List<Subscription> Subscribers { get; set; }
    }
}