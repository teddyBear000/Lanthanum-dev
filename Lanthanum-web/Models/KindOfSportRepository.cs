using System;
using System.Linq;
using Lanthanum_web.Domain;
using Lanthanum_web.Data;
using Microsoft.EntityFrameworkCore;

namespace Lanthanum_web.Models
{
    public class KindOfSportRepository : IRepository<KindOfSport>
    {
        private readonly ApplicationContext context;
        private bool disposed = false;
        public KindOfSportRepository()
        {
            this.context = new ApplicationContext();
        }
        public IQueryable<KindOfSport> GetAllItems()
        {
            return context.KindsOfSport;
        }
        public KindOfSport GetItem(int id)
        {
            KindOfSport entity = context.KindsOfSport.Find(id);
            return entity;
        }

        public void AddItem(KindOfSport entity)
        {
            if (entity.Id == default)
            {
                context.Entry(entity).State = EntityState.Added;
                Save();
            }
        }

        public void UpdateItem(KindOfSport entity)
        {
            if (entity.Id != default)
            {
                context.Entry(entity).State = EntityState.Modified;
                Save();
            }
        }

        public void DeleteItem(int id)
        {
            KindOfSport entity = context.KindsOfSport.Find(id);
            if (entity != null)
            {
                context.KindsOfSport.Remove(entity);
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
