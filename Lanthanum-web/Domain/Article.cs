using System;
using System.Collections.Generic;

namespace Lanthanum_web.Domain
{
    public class Article
    {
        public int Id { get; set; }
        public List<User> Authors { get; set; }
        public string Headline { get; set; }
        public string LogoPath { get; set; }
        public string MainText { get; set; }
        public List<KindOfSport> KindsOfSport { get; set; }
        public List<Team> Teams { get; set; }
        public List<Comment> Comments { get; set; }
        public int ViewsCount { get; set; }
        public int CommentsCount { get; set; }
        public DateTime DateTimeOfCreation { get; set; }
    }
}
