using HamaraBasketRuleEngine.Domain;
using HamaraBasketRuleEngine.Interface;
using HamaraBasketRuleEngine.Model;
using System;

namespace HamaraBasketRuleEngine.Business
{
    public class QualityChangeParams : IQualityChangeParams
    {
        private IRuleByDayLeft _ruleByDayLeft;
        public QualityChangeParams(IRuleByDayLeft ruleByDayLeft)
        {
            _ruleByDayLeft = ruleByDayLeft;
        }
        public QualityParameterModel GetParamters(Item item)
        {
            try
            {
                QualityParameterModel qualityParams = null;
                int qualityChangeByDayLeft = 0;
                if (item.RuleByItem.RuleByNumberOfDaysLeft.Count == 0)
                {
                    qualityChangeByDayLeft = item.RuleByItem.QualityChangesOverApproachingSellByDay;
                }
                else
                {
                    qualityChangeByDayLeft = _ruleByDayLeft.GetRuleByDayLeft(item);
                    if (qualityChangeByDayLeft == 0)
                    {
                        qualityChangeByDayLeft = item.RuleByItem.QualityChangesOverApproachingSellByDay;
                    }
                }
                qualityParams = new QualityParameterModel
                {
                    SellBy = item.SellBy,
                    CurrentQuality = item.Quality,
                    MaxQualityValue = item.RuleByItem.MaxQualityValue,
                    MinQualityValue = item.RuleByItem.MinQualityValue,
                    QualityChangesAfterSellByDate = item.RuleByItem.QualityChangesAfterSellByDay,
                    QualityChangesOverApproachingSellByDate = qualityChangeByDayLeft
                };
                return qualityParams;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
