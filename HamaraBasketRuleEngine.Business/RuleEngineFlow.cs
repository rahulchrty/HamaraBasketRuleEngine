using HamaraBasketRuleEngine.Domain;
using HamaraBasketRuleEngine.Interface;
using System.Collections.Generic;

namespace HamaraBasketRuleEngine.Business
{
    public class RuleEngineFlow : IRuleEngineFlow
    {
        private IItemWithRules _itemWithRules;
        private IExecutingRules _executingRules;
        private IQualityUpdationRepository _qualityUpdationRepository;
        public RuleEngineFlow(IItemWithRules itemWithRules,
                            IExecutingRules executingRules,
                            IQualityUpdationRepository qualityUpdationRepository)
        {
            _itemWithRules = itemWithRules;
            _executingRules = executingRules;
            _qualityUpdationRepository = qualityUpdationRepository;
        }
        public void Execute()
        {
            List<Item> itemsWithRules = _itemWithRules.GetItemWithRules();
            if (itemsWithRules != null)
            {
                List<Item> itemsWithUpdatedQualityValues = _executingRules.Execute(itemsWithRules);
                _qualityUpdationRepository.Update(itemsWithUpdatedQualityValues);
            }
        }
    }
}