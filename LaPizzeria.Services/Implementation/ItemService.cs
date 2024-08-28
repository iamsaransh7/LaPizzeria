using LaPizzeria.Core.Entities;
using LaPizzeria.Models;
using LaPizzeria.Repositories.Interfaces;
using LaPizzeria.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaPizzeria.Services.Implementation
{
    public class ItemService : Service<Item>, IItemService
    {
        IRepository<Item> _itemRepo;
        public ItemService(IRepository<Item> itemRepo): base(itemRepo)
        {
            _itemRepo = itemRepo;
        }

        public IEnumerable<ItemModel> GetItems()
        {
            var data = _itemRepo.GetAll().OrderBy(item => item.CategoryId).Select(i => new ItemModel 
            {
                Id = i.Id,
                Name = i.Name,
                CategoryId = i.CategoryId,
                Description = i.Description,
                ImageUrl = i.ImageUrl,
                ItemTypeId = i.ItemTypeId,
                UnitPrice = i.UnitPrice
            });
            return data;
        }
    }
}
