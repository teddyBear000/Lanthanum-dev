using System;

namespace Lanthanum_web.Domain
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public KindOfSport KindOfSport { get; set; }
        public DateTime DateTimeOfFoundation { get; set; }
    }
}