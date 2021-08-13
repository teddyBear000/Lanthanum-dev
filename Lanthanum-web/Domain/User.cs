using System;
using System.Collections.Generic;

namespace Lanthanum_web.Domain
{
    public enum UserStates
    {
        Activated,
        InActivated,
        Banned
    }

    public enum RoleStates
    {
        User,
        Editor,
        Admin
    }

    public enum CurrentStates
    {
        Online,
        Offline
    }

    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public UserStates UserState { get; set; }
        public CurrentStates CurrentState { get; set; }
        public string PasswordHash { get; set; }
        public RoleStates Role { get; set; }

        public int SubscriptionID { get; set; }
        public List<Subscription> Subscriptions { get; set; }
        public List<Article> Articles { get; set; }
    }
}
