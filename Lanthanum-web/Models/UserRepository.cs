using System;
using System.Linq;
using Lanthanum_web.Domain;
using Lanthanum_web.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Lanthanum_web.Models
{
    public class UserRepository : IRepository<User>
    {
        private readonly ApplicationContext context;
        private bool disposed = false;
        public UserRepository()
        {
            this.context = new ApplicationContext();
        }

        public IEnumerable<User> GetAllItems()
        {
            return context.Users;
        }
        public User GetItem(int id)
        {
            User entity = context.Users.Find(id);

            return entity;
        }

        public void CreateSubscribtion(User OldEntity, User NewEntity)
        {
            var subscribtionEntity = new SubscriptionRepository().AddItem(new Subscription { UserID = NewEntity.Id });
            OldEntity.SubscriptionID = subscribtionEntity.Id;
            Save();
        }

        public User AddItem(User entity)
        {
            User newEntity = null;

            if (entity.Id == default)
            {
                context.Entry(entity).State = EntityState.Added;
                Save();

                newEntity = (User)(context.Entry(entity).GetDatabaseValues().ToObject());
                CreateSubscribtion(entity, newEntity);
            }

            return newEntity;
        }

        public void UpdateItem(User entity)
        {
            if (entity.Id != default)
            {
                context.Entry(entity).State = EntityState.Modified;
                Save();
            }
        }

        public void DeleteItem(int id)
        {
            User entity = context.Users.Find(id);
            if (entity != null)
            {
                context.Users.Remove(entity);
                Save();
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }


        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
