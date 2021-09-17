using Lanthanum.Data;

namespace Lanthanum.Web.Domain
{
    public class Reaction: IEntity
    {
        public int Id { get; init; }
        public ReactionType TypeOfReaction { get; set; }

        public User Author { get; set; }
        public Comment Comment { get; set; }
    }
    public enum ReactionType
    {
        Like,
        Dislike
    }
}