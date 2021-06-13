using HamaraBasketRuleEngine.Domain;
using System.Collections.Generic;

namespace HamaraBasketRuleEngine.Interface
{
    public interface IItemRepository
    {
        List<Item> FetchItems();
    }
}
