using System.Collections.Generic;
using Lanthanum.Web.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Lanthanum.Web.Services
{
    public interface IFooterService
    {
        public IEnumerable<FooterItem> GetAllItems();
        public void AddItem(FooterItem toBeAdded);
        public void UpdateItem(string itemName, string change);
        public void RemoveItem(string itemName);
        public bool HideItem(string itemName);
        public bool UnhideItem(string itemName);
        public FooterItem GetSingleItem(string itemName);
    }
}
