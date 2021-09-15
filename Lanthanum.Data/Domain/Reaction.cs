using Lanthanum.Data;

namespace Lanthanum.Web.Domain
{
    public class Reaction: IEntity
    {
        public int Id { get; init; }
        public ReactionType State { get; set; }

        public User Author { get; set; }
    }
    public enum ReactionType
    {
        Like,
        Dislike
    }
}