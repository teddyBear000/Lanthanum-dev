using System;
using System.Collections.Generic;

namespace Lanthanum_web.Domain
{
    public class Comment
    {
        public int Id { get; set; }
        public User Author { get; set; }
        public Article Article { get; set; }
        public string Content { get; set; }
        public DateTime DateTimeOfCreation { get; set; }
        public Comment ParentComment { get; set; }
        public List<Reaction> Reactions { get; set; }
    }
}
