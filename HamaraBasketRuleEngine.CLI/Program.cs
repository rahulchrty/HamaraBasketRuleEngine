using HamaraBasketRuleEngine.Business;
using HamaraBasketRuleEngine.Interface;
using HamaraBasketRuleEngine.Repository;
using System;
using System.Configuration;

namespace HamaraBasketRuleEngine.CLI
{
    public class Program
    {
        public static void Main (string[] args)
        {
            #region DEPENDENCIES
            var hamaraBasketRuleEngine = new HamaraBasketRuleEngineContext();
            IItemRepository itemRepository = new ItemRepository(hamaraBasketRuleEngine);
            IItemWithRules itemWithRules = new ItemWithRules(itemRepository);
            ISellByChange sellByChange = new SellByChange();
            IQualityChange qualityChange = new QualityChange();
            IRuleByDayLeft ruleByDayLeft = new RuleByDayLeft();
            IQualityChangeParams qualityChangeParams = new QualityChangeParams(ruleByDayLeft);
            IExecutingRules executingRules = new ExecutingRules(sellByChange, qualityChangeParams, qualityChange);
            IQualityUpdationRepository qualityUpdationRepository = new QualityUpdationRepository(hamaraBasketRuleEngine);
            IRuleEngineFlow ruleEngineFlow = new RuleEngineFlow(itemWithRules, executingRules, qualityUpdationRepository);
            #endregion

            ruleEngineFlow.Execute();

            string b = ConfigurationManager.ConnectionStrings["HamaraBasketDbConnection"].ToString();
            Console.WriteLine(b);
        }
    }
}
