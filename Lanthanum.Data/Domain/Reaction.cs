namespace Lanthanum.Data.Domain
{
    public enum ReactionStates
    {
        Liked,
        Disliked
    }
    public class Reaction: IEntity
    {
        public int Id { get; init; }
        public ReactionStates State { get; set; }

        public User Author { get; set; }
        public Comment Comment { get; set; }
    }
}