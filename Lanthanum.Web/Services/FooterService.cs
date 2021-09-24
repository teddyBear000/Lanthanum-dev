using System.Collections.Generic;
using Lanthanum.Web.Data.Repositories;
using Lanthanum.Web.Services;
using Lanthanum.Web.Domain;

namespace Lanthanum.Web.Models
{
    public class FooterService: IFooterService
    {
        public readonly DbRepository<FooterTabItem> _repository;

        public FooterService(DbRepository<FooterTabItem> repository)
        {
            _repository = repository;
        }

        public IEnumerable<FooterTabItem> GetAllItems()
        {
            return _repository.GetAllAsync().Result;
        }

        public void AddItem(FooterTabItem toBeAdded)
        {
            _repository.AddAsync(toBeAdded).Wait();
        }

        public void UpdateItem(string itemName, string change)
        {
            var dataToBeUpdated = _repository.SingleOrDefaultAsync(x => x.Name == itemName).Result;
            dataToBeUpdated.Name = change;
            _repository.UpdateAsync(dataToBeUpdated).Wait();
        }

        public bool HideItem(string itemName)
        {
            var dataToBeUpdated = _repository.SingleOrDefaultAsync(x => x.Name == itemName).Result;
            dataToBeUpdated.IsDisplaying = false;
            _repository.UpdateAsync(dataToBeUpdated).Wait();
            if (_repository.SingleOrDefaultAsync(x => x.Name == itemName).Result.IsDisplaying == false)
            {
                return true;
            }
            return true;

        }
        
        public bool UnhideItem(string itemName)
        {
            var dataToBeUpdated = _repository.SingleOrDefaultAsync(x => x.Name == itemName).Result;
            dataToBeUpdated.IsDisplaying = true;
            _repository.UpdateAsync(dataToBeUpdated).Wait();
            if(_repository.SingleOrDefaultAsync(x => x.Name == itemName).Result.IsDisplaying == true)
            {
                return true;   
            }
            return true;

        }

        public void RemoveItem(string itemName)
        {
            _repository.RemoveAsync(_repository.SingleOrDefaultAsync(x => x.Name == itemName).Result).Wait();
        }

        public IEnumerable<FooterTabItem> FindItem(string itemName)
        {
            return _repository.Find(x => x.Name == itemName);
        }
    }
}
