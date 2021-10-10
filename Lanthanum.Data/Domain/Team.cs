using System;
using System.Collections.Generic;
using Lanthanum.Data;

namespace Lanthanum.Web.Domain
{
    public class Team: IEntity
    {
        public int Id { get; init; }
        public string Name { get; set; }
        public string Location { get; set; }

        public string Conference { get; set; }
        public DateTime DateTimeOfFoundation { get; set; }

        public KindOfSport KindOfSport { get; set; }
        public List<Article> Articles { get; set; }
        public List<Subscription> Subscribers { get; set; }
    }
}