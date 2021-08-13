using System.Linq;
using Lanthanum_web.Domain;
using Lanthanum_web.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace Lanthanum_web.Models
{
    public class ArticleRepository : IRepository<Article>
    {
        private readonly ApplicationContext context;
        private bool disposed = false;
        public ArticleRepository()
        {
            this.context = new ApplicationContext();
        }
        public IQueryable<Article> GetAllItems()
        {
            return context.Articles;
        }
        public Article GetItem(int id)
        {
            Article entity = context.Articles.Find(id);
            return entity;
        }

        public void AddItem(Article entity)
        {
            if (entity.Id == default)
            {
                context.Entry(entity).State = EntityState.Added;
                Save();
            }
        }

        public void UpdateItem(Article entity)
        {
            if (entity.Id != default)
            {
                context.Entry(entity).State = EntityState.Modified;
                Save();
            }
        }

        public void DeleteItem(int id)
        {
            Article entity = context.Articles.Find(id);
            if (entity != null)
            {
                context.Articles.Remove(entity);
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
