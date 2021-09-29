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

        public void UpdateItem(string itemNameForUpdate, string nameChangeForUpdate, string contentChangeForUpdate)
        {
            var currentId = _repository.SingleOrDefaultAsync(x => x.Name == itemNameForUpdate).Result.Id;
            var dataToBeUpdated = _repository.GetByIdAsync(currentId).Result;
            dataToBeUpdated.Content = contentChangeForUpdate;
            dataToBeUpdated.Name = nameChangeForUpdate;
            _repository.UpdateAsync(dataToBeUpdated).Wait();
        }

        public void HideUnhideItem(string itemName)
        {
            var dataToBeUpdated = _repository.SingleOrDefaultAsync(x => x.Name == itemName).Result;
            dataToBeUpdated.IsDisplaying = !dataToBeUpdated.IsDisplaying;
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

        public void HideUnhideAllItemsInCategory(string currentTab)
        {
            var dataToBeUpdated = _repository.SingleOrDefaultAsync(x => x.Name == currentTab && x.Category== currentTab).Result;
            dataToBeUpdated.IsDisplaying = !dataToBeUpdated.IsDisplaying;
            _repository.UpdateAsync(dataToBeUpdated).Wait();
        }
    }
}
