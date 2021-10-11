using System.Collections.Generic;
using Lanthanum.Web.Domain;

namespace Lanthanum.Web.Services
{
    public interface IFooterService
    {
        public IEnumerable<FooterItem> GetAllItems();
        public FooterItem GetSingleItem(string itemName);
        public void AddItem(FooterItem toBeAdded);
        public void UpdateItem(string itemNameForUpdate, string nameChangeForUpdate, string contentChangeForUpdate);
        public void RemoveItem(string itemName);
        public void HideUnhideItem(string itemName);
        public void HideUnhideAllItemsInCategory(string currentTab);
    }
}
