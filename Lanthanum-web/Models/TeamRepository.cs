﻿using System;
using System.Linq;
using Lanthanum_web.Domain;
using Lanthanum_web.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
        public IEnumerable<Team> GetAllItems()
        {
            return context.Teams;
        }
        public Team GetItem(int id)
        {
            Team entity = context.Teams.Find(id);
            return entity;
        }

        public Team AddItem(Team entity)
        {
            Team newEntity = null;
            if (entity.Id == default)
            {
                context.Entry(entity).State = EntityState.Added;
                Save();

                newEntity = (Team)(context.Entry(entity).GetDatabaseValues().ToObject());
            }

            return newEntity;
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
