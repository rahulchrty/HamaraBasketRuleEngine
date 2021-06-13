using HamaraBasketRuleEngine.Interface;
using System;

namespace HamaraBasketRuleEngine.Business
{
    public class SellByChange : ISellByChange
    {
        public int? ApplyRule(int? currentSellByValue)
        {
            try
            {
                if (currentSellByValue != null)
                {
                    currentSellByValue -= 1;
                }
                return currentSellByValue;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
