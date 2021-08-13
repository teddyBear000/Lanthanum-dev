using System;
using System.Linq;
using Lanthanum_web.Domain;
using Lanthanum_web.Data;
using Microsoft.EntityFrameworkCore;

namespace Lanthanum_web.Models
{
    public class TeamRepository : IRepository<Team>
    {
        private readonly ApplicationContext context;
        private bool disposed = false;
        public TeamRepository()
        {
            this.context = new ApplicationContext();
        }
        public IQueryable<Team> GetAllItems()
        {
            return context.Teams;
        }
        public Team GetItem(int id)
        {
            Team entity = context.Teams.Find(id);
            return entity;
        }

        public void AddItem(Team entity)
        {
            if (entity.Id == default)
            {
                context.Entry(entity).State = EntityState.Added;
                Save();
            }
        }

        public void UpdateItem(Team entity)
        {
            if (entity.Id != default)
            {
                context.Entry(entity).State = EntityState.Modified;
                Save();
            }
        }

        public void DeleteItem(int id)
        {
            Team entity = context.Teams.Find(id);
            if (entity != null)
            {
                context.Teams.Remove(entity);
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
