using System;
using System.Linq;
using Lanthanum_web.Domain;
using Lanthanum_web.Data;
using Microsoft.EntityFrameworkCore;

namespace Lanthanum_web.Models
{
    public class UserRepository : IRepository<User>
    {
        private readonly ApplicationContext context;
        private bool disposed = false;
        public UserRepository(ApplicationContext context)
        {
            this.context = context;
        }
        public IQueryable<User> GetAllItems()
        {
            return context.Users;
        }
        public User GetItem(int id)
        {
            User entity = context.Users.Find(id);
            return entity;
        }

        public void AddItem(User entity)
        {
            if (entity.Id == default)
            {
                context.Entry(entity).State = EntityState.Added;
                Save();
            }
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
