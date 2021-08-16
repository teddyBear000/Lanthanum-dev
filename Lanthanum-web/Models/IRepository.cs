﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Lanthanum_web.Models
{
    interface IRepository<T>: IDisposable
        where T: class
    {
        public IEnumerable<T> GetAllItems();
        public T GetItem(int id);
        public void AddItem(T entity);
        public void UpdateItem(T entity);
        public void DeleteItem(int id);
        public void Save();
    }
}
