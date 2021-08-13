using System;
using System.Linq;
using Lanthanum_web.Domain;
using Lanthanum_web.Data;
using Microsoft.EntityFrameworkCore;

namespace Lanthanum_web.Models
{
    public class ReactionRepository : IRepository<Reaction>
    {
        private readonly ApplicationContext context;
        private bool disposed = false;
        public ReactionRepository()
        {
            this.context = new ApplicationContext();
        }
        public IQueryable<Reaction> GetAllItems()
        {
            return context.Reactions;
        }
        public Reaction GetItem(int id)
        {
            Reaction entity = context.Reactions.Find(id);
            return entity;
        }

        public void AddItem(Reaction entity)
        {
            if (entity.Id == default)
            {
                context.Entry(entity).State = EntityState.Added;
                Save();
            }
        }

        public void UpdateItem(Reaction entity)
        {
            if (entity.Id != default)
            {
                context.Entry(entity).State = EntityState.Modified;
                Save();
            }
        }

        public void DeleteItem(int id)
        {
            Reaction entity = context.Reactions.Find(id);
            if (entity != null)
            {
                context.Reactions.Remove(entity);
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
