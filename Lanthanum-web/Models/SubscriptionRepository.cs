using System;
using System.Linq;
using Lanthanum_web.Domain;
using Lanthanum_web.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Lanthanum_web.Models
{
    public class SubscriptionRepository : IRepository<Subscription>
    {
        private readonly ApplicationContext context;
        private bool disposed = false;
        public SubscriptionRepository()
        {
            this.context = new ApplicationContext();
        }
        public IEnumerable<Subscription> GetAllItems()
        {
            return context.Subscriptions;
        }
        public Subscription GetItem(int id)
        {
            Subscription entity = context.Subscriptions.Find(id);
            return entity;
        }

        public Subscription AddItem(Subscription entity)
        {
            Subscription newEntity = null;

            if (entity.Id == default)
            {
                context.Entry(entity).State = EntityState.Added;
                Save();

                newEntity = (Subscription)(context.Entry(entity).GetDatabaseValues().ToObject());
            }

            return newEntity;
        }

        public void UpdateItem(Subscription entity)
        {
            if (entity.Id != default)
            {
                context.Entry(entity).State = EntityState.Modified;
                Save();
            }
        }

        public void DeleteItem(int id)
        {
            Subscription entity = context.Subscriptions.Find(id);
            if (entity != null)
            {
                context.Subscriptions.Remove(entity);
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
