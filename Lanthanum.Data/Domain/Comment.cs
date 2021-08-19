using System;
using System.Collections.Generic;
using Lanthanum.Data;

namespace Lanthanum.Web.Domain
{
    public class Comment: IEntity
    {
        public int Id { get; init; }
        public User Author { get; set; }
        public Article Article { get; set; }
        public string Content { get; set; }
        public DateTime DateTimeOfCreation { get; set; }
        public Comment ParentComment { get; set; }
        public List<Reaction> Reactions { get; set; }
    }
}
