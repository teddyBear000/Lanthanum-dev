using System.Collections.Generic;
using Lanthanum.Web.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Lanthanum.Web.Services
{
    public interface IFooterService
    {
        public IEnumerable<FooterTabItem> GetAllItems();
        public void AddItem(FooterTabItem toBeAdded);
        public void UpdateItem(string itemName, string change);
        public void RemoveItem(string itemName);
        public bool HideItem(string itemName);
        public bool UnhideItem(string itemName);
        public FooterTabItem GetSingleItem(string itemName);
    }
}
