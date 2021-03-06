using System;
using Lanthanum.Data;

namespace Lanthanum.Web.Domain
{
    public class Ban: IEntity
    {
        public int Id { get; init; }
        public User BannedUser { get; set; }
        public string Reason { get; set; }
        public DateTime DateOfBan { get; set; }
        public User BanAuthor { get; set; }
        public bool IsActive { get; set; }
    }
}