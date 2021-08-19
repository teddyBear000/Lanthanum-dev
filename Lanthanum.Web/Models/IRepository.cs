using System;
using System.Collections.Generic;
using System.Linq;

namespace Lanthanum.Web.Models
{
    interface IRepository<T>: IDisposable
        where T: class
    {
        public IEnumerable<T> GetAllItems();
        public T GetItem(int id);
        public T AddItem(T entity);
        public void UpdateItem(T entity);
        public void DeleteItem(int id);
        public void Save();
    }
}
