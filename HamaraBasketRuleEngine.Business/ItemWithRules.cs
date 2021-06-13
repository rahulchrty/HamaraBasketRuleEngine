using HamaraBasketRuleEngine.Interface;
using HamaraBasketRuleEngine.Domain;
using System.Collections.Generic;
using System;

namespace HamaraBasketRuleEngine.Business
{
    public class ItemWithRules : IItemWithRules
    {
        private IItemRepository _itemRepository;
        public ItemWithRules(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        public List<Item> GetItemWithRules()
        {
            try
            {
                List<Item> itemList = _itemRepository.FetchItems();
                return itemList;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
