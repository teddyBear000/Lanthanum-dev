using System.Linq;
using Lanthanum_web.Domain;
using Lanthanum_web.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

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
        public IEnumerable<Article> GetAllItems()
        {
            return context.Articles;
        }
        public Article GetItem(int id)
        {
            Article entity = context.Articles.Find(id);
            return entity;
        }

        public bool TryAddItem(Article entity)
        {
            try 
            {
                entity = AddItem(entity);
                return true;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public Article AddItem(Article entity)
        {
            if (entity.Id != default)
                throw new ArgumentException("Wrong ID");
     
            var element = context.Entry(entity);
            element.State = EntityState.Added;
            Save();

            return element.Entity;
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
