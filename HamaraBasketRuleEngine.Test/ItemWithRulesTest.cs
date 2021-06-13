using HamaraBasketRuleEngine.Business;
using HamaraBasketRuleEngine.Domain;
using HamaraBasketRuleEngine.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace HamaraBasketRuleEngine.Test
{
    [TestClass]
    public class ItemWithRulesTest
    {
        private Mock<IItemRepository> _mockItemRepository;
        private IItemWithRules _itemWithRules;

        [TestInitialize]
        public void Setup()
        {
            _mockItemRepository = new Mock<IItemRepository>();
            _itemWithRules = new ItemWithRules(_mockItemRepository.Object);
        }

        [TestMethod]
        public void GetItemWithRules_OnExecution_GetAllItemList()
        {
            //Given:
            //When: I execute GetItemWithRules method
            _mockItemRepository.Setup(x => x.FetchItems()).Returns(new List<Item> { new Item {
                ItemId = 1,
                ItemName = "TestItem",
                Quality = 50,
                SellBy = 5,
                RuleByItem = null
            }});
            var result = _itemWithRules.GetItemWithRules();
            //Then: I get a list of one or more items
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void GetItemWithRules_OnExecution_Method_FetchItems_Also_Executes()
        {
            //Given:
            //When: I execute GetItemWithRules method
            _mockItemRepository.Setup(x => x.FetchItems()).Returns(new List<Item> { new Item {
                ItemId = 1,
                ItemName = "TestItem",
                Quality = 50,
                SellBy = 5,
                RuleByItem = null
            }});
            var result = _itemWithRules.GetItemWithRules();
            //Then: Method FetchItems executes
            _mockItemRepository.Verify(x => x.FetchItems(), Times.Once);
        }
    }
}
