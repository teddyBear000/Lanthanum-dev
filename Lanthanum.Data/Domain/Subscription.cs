using System.Collections.Generic;
using Lanthanum.Data;

namespace Lanthanum.Web.Domain
{
    public class Subscription: IEntity
    {
        public int Id { get; init; }
        
        public User Owner { get; set; }
        public int OwnerId { get; set; }
        public List<KindOfSport> SubscribedSports { get; set; }
        public List<Team> SubscribedTeams { get; set; }
        public List<User> SubscribedAuthors { get; set; }
    }
}