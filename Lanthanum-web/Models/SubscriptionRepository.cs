using System;
using System.Linq;
using Lanthanum_web.Domain;
using Lanthanum_web.Data;
using Microsoft.EntityFrameworkCore;

namespace Lanthanum_web.Models
{
    public class SubscriptionRepository : IRepository<Subscription>
    {
        private readonly ApplicationContext context;
        private bool disposed = false;
        public SubscriptionRepository(ApplicationContext context)
        {
            this.context = context;
        }
        public IQueryable<Subscription> GetAllItems()
        {
            return context.Subscriptions;
        }
        public Subscription GetItem(int id)
        {
            Subscription entity = context.Subscriptions.Find(id);
            return entity;
        }

        public void AddItem(Subscription entity)
        {
            if (entity.Id == default)
            {
                context.Entry(entity).State = EntityState.Added;
                Save();
            }
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
