using HamaraBasketRuleEngine.Interface;
using HamaraBasketRuleEngine.Model;
using System;

namespace HamaraBasketRuleEngine.Business
{
    public class QualityChange : IQualityChange
    {
        public int ApplyRule(QualityParameterModel qualityParameter)
        {
            try
            {
                int qualityAfterApplyRules = (int)qualityParameter.CurrentQuality;
                if (qualityParameter.SellBy != 0
                    && qualityParameter.CurrentQuality > qualityParameter.MinQualityValue
                    && qualityParameter.CurrentQuality < qualityParameter.MaxQualityValue)
                {
                    qualityParameter.CurrentQuality += qualityParameter.QualityChangesOverApproachingSellByDate;
                }
                else if (qualityParameter.SellBy == 0
                    && qualityParameter.CurrentQuality > 1
                    && qualityParameter.CurrentQuality < qualityParameter.MaxQualityValue)
                {
                    qualityAfterApplyRules += qualityParameter.QualityChangesAfterSellByDate;
                }
                else
                {
                    qualityAfterApplyRules = 0;
                }
                return qualityAfterApplyRules;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
