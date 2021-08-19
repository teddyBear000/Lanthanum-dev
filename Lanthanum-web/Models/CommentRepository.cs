using System;
using System.Linq;
using Lanthanum_web.Domain;
using Lanthanum_web.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Lanthanum_web.Models
{
    public class CommentRepository : IRepository<Comment>
    {
        private readonly ApplicationContext context;
        private bool disposed = false;
        public CommentRepository()
        {
            this.context = new ApplicationContext();
        }
        public IEnumerable<Comment> GetAllItems()
        {
            return context.Comments;
        }

        public Comment GetItem(int id)
        {
            Comment entity = context.Comments.Find(id);
            return entity;
        }

        public bool TryAddItem(Comment entity)
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

        public Comment AddItem(Comment entity)
        {
            if (entity.Id != default)
                throw new ArgumentException("Wrong ID");

            var element = context.Entry(entity);
            element.State = EntityState.Added;
            Save();

            return element.Entity;
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
