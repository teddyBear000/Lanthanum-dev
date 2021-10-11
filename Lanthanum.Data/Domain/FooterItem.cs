using Lanthanum.Data;

namespace Lanthanum.Web.Domain
{
    public class FooterItem : IEntity
    {
        public int Id { get; init; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Content { get; set; }
        public bool IsDisplaying { get; set; }
    }
}
