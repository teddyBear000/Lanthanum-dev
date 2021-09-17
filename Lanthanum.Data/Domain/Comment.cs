using System;
using System.Collections.Generic;
using System.Linq;
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
        public int Rate 
        {
            get
            {
                if (Reactions == null) return 0;
                return Reactions.Count(r => r.TypeOfReaction == ReactionType.Like) 
                    - Reactions.Count(r => r.TypeOfReaction == ReactionType.Dislike);
            }
        }
    }
}
