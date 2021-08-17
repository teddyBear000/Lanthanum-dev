using System;
using System.Linq;
using Lanthanum_web.Domain;
using Lanthanum_web.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
        public IEnumerable<Reaction> GetAllItems()
        {
            return context.Reactions;
        }
        public Reaction GetItem(int id)
        {
            Reaction entity = context.Reactions.Find(id);
            return entity;
        }

        public Reaction AddItem(Reaction entity)
        {
            Reaction newEntity = null;
            if (entity.Id == default)
            {
                context.Entry(entity).State = EntityState.Added;
                Save();

                newEntity = (Reaction)(context.Entry(entity).GetDatabaseValues().ToObject());
            }

            return newEntity;
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
