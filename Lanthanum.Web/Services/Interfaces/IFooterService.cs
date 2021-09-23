using System.Collections.Generic;
using Lanthanum.Web.Domain;

namespace Lanthanum.Web.Services
{
    public interface IFooterService
    {
        public IEnumerable<FooterTabItem> GetAllItems();
        public void AddItem(FooterTabItem toBeAdded);
        public void UpdateItem(string itemName, string attributeToChange, string change);
        public void RemoveItem(string itemName);
    }
}
