using HamaraBasketRuleEngine.Domain;
using System.Collections.Generic;

namespace HamaraBasketRuleEngine.Interface
{
    public interface IQualityUpdationRepository
    {
        void Update(List<Item> itemsWithUpdatedQualityValues);
    }
}
