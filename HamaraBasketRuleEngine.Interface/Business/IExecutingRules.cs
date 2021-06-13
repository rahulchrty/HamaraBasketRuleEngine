using HamaraBasketRuleEngine.Domain;
using System.Collections.Generic;

namespace HamaraBasketRuleEngine.Interface
{
    public interface IExecutingRules
    {
        List<Item> Execute(List<Item> itemsWithRules);
    }
}
