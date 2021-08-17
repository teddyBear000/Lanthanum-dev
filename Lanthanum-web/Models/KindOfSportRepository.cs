using System;
using System.Linq;
using Lanthanum_web.Domain;
using Lanthanum_web.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
        public IEnumerable<KindOfSport> GetAllItems()
        {
            return context.KindsOfSport;
        }
        public KindOfSport GetItem(int id)
        {
            KindOfSport entity = context.KindsOfSport.Find(id);
            return entity;
        }

        public KindOfSport AddItem(KindOfSport entity)
        {
            KindOfSport newEntity = null;

            if (entity.Id == default)
            {
                context.Entry(entity).State = EntityState.Added;
                Save();

                newEntity = (KindOfSport)(context.Entry(entity).GetDatabaseValues().ToObject());
            }

            return newEntity;
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
