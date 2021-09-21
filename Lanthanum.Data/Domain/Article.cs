using System;
using System.Collections.Generic;
using Lanthanum.Data;

namespace Lanthanum.Web.Domain
{
    public class Article: IEntity
    {
        public int Id { get; init; }
        public List<User> Authors { get; set; }
        public string Headline { get; set; }
        public string Header { get; set; }
        public string LogoPath { get; set; }
        public string MainText { get; set; }
        //public string Location { get; set; }
        //public string Alternative { get; set; }
        public List<KindOfSport> KindsOfSports { get; set; }
        public List<Team> Teams { get; set; }
        public List<Comment> Comments { get; set; }
        public int ViewsCount { get; set; }
        public int CommentsCount { get; set; }
        public DateTime DateTimeOfCreation { get; set; }
    }
}
