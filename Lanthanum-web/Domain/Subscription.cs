using System.Collections.Generic;

namespace Lanthanum_web.Domain
{
    public class Subscription
    {
        public int Id { get; set; }
        public List<KindOfSport> SubscribedSports { get; set; }
        public List<Team> SubscribedTeams { get; set; }
        public List<User> SubscribedAuthors { get; set; }
    }

}