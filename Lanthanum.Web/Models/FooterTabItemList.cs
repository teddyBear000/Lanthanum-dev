using System.Collections.Generic;
using Lanthanum.Web.Data.Repositories;
using Lanthanum.Web.Domain;

namespace Lanthanum.Web.Models
{
    public class FooterTabItemList
    {
        public readonly DbRepository<FooterTabItem> _repository;

        public FooterTabItemList(DbRepository<FooterTabItem> repository)
        {
            _repository = repository;
        }

        public IEnumerable<FooterTabItem> GetItems()
        {
            return _repository.GetAllAsync().Result;
        }

        public void AddItem(FooterTabItem toBeAdded)
        {
            _repository.AddAsync(toBeAdded).Wait();

        }
        public void RemoveItem(FooterTabItem toBeDeleted)
        {
            _repository.RemoveAsync(toBeDeleted).Wait();
        } 
    }
}
