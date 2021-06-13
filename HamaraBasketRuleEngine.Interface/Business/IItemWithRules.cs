using HamaraBasketRuleEngine.Domain;
using System.Collections.Generic;

namespace HamaraBasketRuleEngine.Interface
{
    public interface IItemWithRules
    {
        List<Item> GetItemWithRules();
    }
}
