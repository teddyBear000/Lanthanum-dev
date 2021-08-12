using System;
using System.Linq;
using Lanthanum_web.Domain;
using Lanthanum_web.Data;
using Microsoft.EntityFrameworkCore;

namespace Lanthanum_web.Models
{
    public class CommentRepository : IRepository<Comment>
    {
        private readonly ApplicationContext context;
        private bool disposed = false;
        public CommentRepository(ApplicationContext context)
        {
            this.context = context;
        }
        public IQueryable<Comment> GetAllItems()
        {
            return context.Comments;
        }
        public Comment GetItem(int id)
        {
            Comment entity = context.Comments.Find(id);
            return entity;
        }

        public void AddItem(Comment entity)
        {
            if (entity.Id == default)
            {
                context.Entry(entity).State = EntityState.Added;
                Save();
            }
        }

        public void UpdateItem(Comment entity)
        {
            if (entity.Id != default)
            {
                context.Entry(entity).State = EntityState.Modified;
                Save();
            }
        }

        public void DeleteItem(int id)
        {
            Comment entity = context.Comments.Find(id);
            if (entity != null)
            {
                context.Comments.Remove(entity);
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
