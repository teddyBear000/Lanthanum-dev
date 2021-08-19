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

        public void CreateSubscribtion(User Entity)
        {
            var subscribtionEntity = new SubscriptionRepository().AddItem(new Subscription { UserID = Entity.Id });
            Entity.SubscriptionID = subscribtionEntity.Id;
            Save();
        }
        public bool TryAddItem(User entity)
        {
            try
            {
                entity = AddItem(entity);
                return true;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public User AddItem(User entity)
        {
            if (entity.Id != default)
                throw new ArgumentException("Wrong ID");

            var element = context.Entry(entity);
            element.State = EntityState.Added;
            Save();

            CreateSubscribtion(element.Entity);

            return element.Entity;
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
