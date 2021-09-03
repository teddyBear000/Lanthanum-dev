using System;
using System.Collections.Generic;
using Lanthanum.Data;

namespace Lanthanum.Web.Domain
{
    public class User: IEntity
    {
        public int Id { get; init; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; init; }
        public bool IsBaned { get; set; }
        public CurrentStates CurrentState { get; set; }
        public string PasswordHash { get; set; }
        public RoleStates Role { get; set; }
        public Subscription Subscription { get; set; }
        public List<Subscription> Subscribers { get; set; }
        public List<Article> PublishedArticles { get; set; }
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
}
