using System;
using Lanthanum.Data;

namespace Lanthanum.Data.Domain
{
    public class ActionRequest: IEntity
    {
        public int Id { get; init; }
        public DateTime DateTimeOfCreation { get; init; }
        public string RequestCode { get; init; }
        public User RequestOwner { get; init; }

        public ActionRequest ShallowCopy()
        {
            return (ActionRequest)MemberwiseClone();
        }
    }
}