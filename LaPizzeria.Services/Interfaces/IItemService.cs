using LaPizzeria.Core.Entities;
using LaPizzeria.Models;

namespace LaPizzeria.Services.Interfaces
{
    public interface IItemService: IService<Item>
    {
        public IEnumerable<ItemModel> GetItems();
    }
}
