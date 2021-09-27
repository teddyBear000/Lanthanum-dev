using System.Collections.Generic;
using Lanthanum.Web.Domain;

namespace Lanthanum.Web.Services
{
    public interface IFooterService
    {
        public IEnumerable<FooterItem> GetAllItems();
        public FooterItem GetSingleItem(string itemName);
        public void AddItem(FooterItem toBeAdded);
        public void UpdateItem(string itemName, string attributeToChange, string change);
        public void RemoveItem(string itemName);
        public void HideItem(string itemName);
        public void UnhideItem(string itemName);
    }
}
