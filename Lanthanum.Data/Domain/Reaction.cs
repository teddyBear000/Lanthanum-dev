namespace Lanthanum.Web.Data.Domain
{
    public class Reaction : IEntity
    {
        public int Id { get; init; }
        public ReactionStates State { get; set; }

        public User Author { get; set; }
        public Comment Comment { get; set; }
    }
}