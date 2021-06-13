using HamaraBasketRuleEngine.Domain;
using HamaraBasketRuleEngine.Interface;
using System;
using System.Linq;

namespace HamaraBasketRuleEngine.Business
{
    public class RuleByDayLeft : IRuleByDayLeft
    {
        public int GetRuleByDayLeft(Item item)
        {
            try
            {
                int qualityChangeDayLeft = 0;
                qualityChangeDayLeft = item.RuleByItem.RuleByNumberOfDaysLeft
                    .OrderByDescending(x => x.DaysLeftToSellByDate)
                    .Where(x => item.SellBy <= x.DaysLeftToSellByDate)
                    .OrderBy(x => x.DaysLeftToSellByDate)
                    .Select(x => x.QualityChangesByDaysLeft)
                    .FirstOrDefault();
                return qualityChangeDayLeft;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
