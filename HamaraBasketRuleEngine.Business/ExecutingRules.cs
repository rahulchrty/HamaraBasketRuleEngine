using HamaraBasketRuleEngine.Domain;
using HamaraBasketRuleEngine.Interface;
using HamaraBasketRuleEngine.Model;
using System;
using System.Collections.Generic;

namespace HamaraBasketRuleEngine.Business
{
    public class ExecutingRules : IExecutingRules
    {
        private ISellByChange _sellByChange;
        private IQualityChange _qualityChange;
        private IQualityChangeParams _qualityChangeParams;
        public ExecutingRules(ISellByChange sellByChange,
                                IQualityChangeParams qualityChangeParams,
                                IQualityChange qualityChange)
        {
            _sellByChange = sellByChange;
            _qualityChange = qualityChange;
            _qualityChangeParams = qualityChangeParams;
        }
        public List<Item> Execute(List<Item> itemsWithRules)
        {
            try
            {
                foreach (Item eachItem in itemsWithRules)
                {
                    if (eachItem.Quality != null && eachItem.SellBy != null)
                    {
                        eachItem.SellBy = _sellByChange.ApplyRule(eachItem.Quality);
                        QualityParameterModel qualityParams = _qualityChangeParams.GetParamters(eachItem);
                        eachItem.Quality = _qualityChange.ApplyRule(qualityParams);
                    }
                }
                return itemsWithRules;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
