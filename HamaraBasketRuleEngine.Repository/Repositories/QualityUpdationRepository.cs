using HamaraBasketRuleEngine.Domain;
using HamaraBasketRuleEngine.Interface;
using System.Collections.Generic;

namespace HamaraBasketRuleEngine.Repository
{
    public class QualityUpdationRepository : IQualityUpdationRepository
    {
        private HamaraBasketRuleEngineContext _hamaraBasketRuleEngine;
        public QualityUpdationRepository(HamaraBasketRuleEngineContext hamaraBasketRuleEngine)
        {
            _hamaraBasketRuleEngine = hamaraBasketRuleEngine;
        }
        public void Update(List<Item> itemsWithUpdatedQualityValues)
        {
            _hamaraBasketRuleEngine.Items.UpdateRange(itemsWithUpdatedQualityValues);
            _hamaraBasketRuleEngine.SaveChanges();
        }
    }
}
