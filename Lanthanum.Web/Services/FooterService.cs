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

        public void UpdateItem(string itemName, string attributeToChange, string change)

        {
            var dataToBeUpdated = _repository.SingleOrDefaultAsync(x => x.Name == itemName).Result;
            switch (attributeToChange)
            {
                case "Name":
                    dataToBeUpdated.Name = change;
                    _repository.UpdateAsync(dataToBeUpdated).Wait();
                    break;

                case "IsDisplaying":
                    if (change == "True")
                    {
                        dataToBeUpdated.IsDisplaying = true;
                    }
                    else if (change == "False")
                    {
                        dataToBeUpdated.IsDisplaying = false;
                    }
                    else
                    {
                        break;
                    }

                    _repository.UpdateAsync(dataToBeUpdated).Wait();
                    break;
            }
        }

        public void RemoveItem(string itemName)
        {
            _repository.RemoveAsync(_repository.SingleOrDefaultAsync(x => x.Name == itemName).Result).Wait();
        }
    }
}
