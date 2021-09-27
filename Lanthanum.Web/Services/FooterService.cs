using System.Collections.Generic;
using Lanthanum.Web.Data.Repositories;
using Lanthanum.Web.Services;
using Lanthanum.Web.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Lanthanum.Web.Models
{
    public class FooterService: IFooterService 
    {
        public readonly DbRepository<FooterItem> _repository;

        public FooterService(DbRepository<FooterItem> repository)
        {
            _repository = repository;
        }

        public IEnumerable<FooterItem> GetAllItems()
        {
            return _repository.GetAllAsync().Result;
        }

        public void AddItem(FooterItem toBeAdded)
        {
            _repository.AddAsync(toBeAdded).Wait();
        }

        public void UpdateItem(string itemName, string attributeToChange, string change)
        {
            var dataToBeUpdated = _repository.SingleOrDefaultAsync(x => x.Name == itemName).Result;
            if (attributeToChange == "Name") dataToBeUpdated.Name = change;
            else if (attributeToChange == "Content") dataToBeUpdated.Content = change;
            _repository.UpdateAsync(dataToBeUpdated).Wait();
        }

        public void HideItem(string itemName)
        {
            var dataToBeUpdated = _repository.SingleOrDefaultAsync(x => x.Name == itemName).Result;
            dataToBeUpdated.IsDisplaying = false;
            _repository.UpdateAsync(dataToBeUpdated).Wait();
        }
        
        public void UnhideItem(string itemName)
        {
            var dataToBeUpdated = _repository.SingleOrDefaultAsync(x => x.Name == itemName).Result;
            dataToBeUpdated.IsDisplaying = true;
            _repository.UpdateAsync(dataToBeUpdated).Wait();
        }

        public void RemoveItem(string itemName)
        {
            _repository.RemoveAsync(_repository.SingleOrDefaultAsync(x => x.Name == itemName).Result).Wait();
        }

        public FooterItem GetSingleItem(string itemName)
        {
            return _repository.SingleOrDefaultAsync(x => x.Name == itemName).Result;
        }

        
    }
}
