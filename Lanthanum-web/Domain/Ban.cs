using System;

namespace Lanthanum_web.Domain
{
    public class Ban
    {
        public int Id { get; set; }
        public User BannedUser { get; set; }
        public string Reason { get; set; }
        public DateTime DateOfBan { get; set; }
        public int BanAuthorID { get; set; }
        public bool IsActive { get; set; }
    }
}