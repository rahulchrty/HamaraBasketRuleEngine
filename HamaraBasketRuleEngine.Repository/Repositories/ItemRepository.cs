using HamaraBasketRuleEngine.Domain;
using HamaraBasketRuleEngine.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HamaraBasketRuleEngine.Repository
{
    public class ItemRepository : IItemRepository
    {
        private HamaraBasketRuleEngineContext _hamaraBasketRuleEngine;
        public ItemRepository(HamaraBasketRuleEngineContext hamaraBasketRuleEngine)
        {
            _hamaraBasketRuleEngine = hamaraBasketRuleEngine;
        }

        public List<Item> FetchItems()
        {
            try
            {
                List<Item> itemList = (from item in _hamaraBasketRuleEngine.Items
                                       select new Item
                                       {
                                           ItemId = item.ItemId,
                                           SellBy = item.SellBy,
                                           Quality = item.Quality,
                                           RuleByItem = item.RuleByItem
                                       }
                                    ).ToList();
                return itemList;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
