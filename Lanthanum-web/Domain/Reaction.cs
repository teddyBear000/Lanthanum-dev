namespace Lanthanum_web.Domain
{
    public enum ReactionStates
    {
        Liked,
        Disliked
    }
    public class Reaction
    {
        public int Id { get; set; }
        public ReactionStates ReactionState { get; set; }

        public User User { get; set; }
        public Comment Comment { get; set; }
    }
}